using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Camera))]
public class CameraBounds : MonoBehaviour
{
	public float mapMinX = 0;
	public float mapMaxX = 50f;
	public float mapMinY = 0;
	public float mapMaxY = 50f;

	private Camera _camera;

	private float minX, maxX, minY, maxY;
	private float horizontalExtent, verticalExtent;
	private float orthographicSize;

	void Awake ()
	{
		_camera = GetComponent<Camera> ();
		orthographicSize = _camera.orthographicSize;
		CalculateLevelBounds ();
	}

	void CalculateLevelBounds ()
	{
		verticalExtent = _camera.orthographicSize;
		horizontalExtent = _camera.orthographicSize * Screen.width / Screen.height;
		minX = mapMinX + horizontalExtent;
		maxX = mapMaxX - horizontalExtent;
		minY = mapMinY + verticalExtent;
		maxY = mapMaxY - verticalExtent;
	}

	void LateUpdate ()
	{
		if (orthographicSize != _camera.orthographicSize) {
			orthographicSize = _camera.orthographicSize;
			CalculateLevelBounds ();
		}
		Vector3 limitedCameraPosition = _camera.transform.position;
		limitedCameraPosition.x = Mathf.Clamp (limitedCameraPosition.x, minX, maxX);
		limitedCameraPosition.y = Mathf.Clamp (limitedCameraPosition.y, minY, maxY);
		_camera.transform.position = limitedCameraPosition;
	}

	void OnDrawGizmos ()
	{
		float mapWidth = mapMaxX - mapMinX;
		float mapHeight = mapMaxY - mapMinY;
		Gizmos.DrawWireCube (new Vector3 (mapMaxX - (mapWidth / 2f), mapMaxY - (mapHeight / 2f), 0), new Vector3 (mapWidth, mapHeight, 0));
	}
}
