using UnityEngine;
using System.Collections;

public class StandaloneEdgeCameraController : MonoBehaviour
{
	public bool enableZoom = true;
	public float minZoom = 1.0f;
	public float maxZoom = 20.0f;

	public bool lockX = false;
	public bool lockY = false;

	public float scrollBoundry = 5.0f;
	public float speed = 10.0f;


	private Camera _camera;

	void Start ()
	{
		Input.simulateMouseWithTouches = false;
		_camera = GetComponent<Camera> ();
		CameraBounds cameraBounds = GetComponent<CameraBounds> ();

		float mapWidth = cameraBounds.mapMaxX - cameraBounds.mapMinX;
		float mapHeight = cameraBounds.mapMaxY - cameraBounds.mapMinY;

		float mapMaxZoom = 0.5f * (mapWidth / _camera.aspect);
		if (mapWidth > mapHeight)
			mapMaxZoom = 0.5f * mapHeight;
		if (maxZoom > mapMaxZoom)
			maxZoom = mapMaxZoom;
		
		if (_camera.orthographicSize > maxZoom)
			_camera.orthographicSize = maxZoom;
	}

	void Update ()
	{
		float x = 0;
		float y = 0;

		if (Input.mousePosition.x > Screen.width - scrollBoundry && !lockX)
			x = speed * Time.deltaTime;
		if (Input.mousePosition.x < 0 + scrollBoundry && !lockX)
			x = -speed * Time.deltaTime;
		if (Input.mousePosition.y > Screen.height - scrollBoundry && !lockY)
			y = speed * Time.deltaTime;
		if (Input.mousePosition.y < 0 + scrollBoundry && !lockY)
			y = -speed * Time.deltaTime;
		
		_camera.transform.position += new Vector3 (x, y, 0);

		if (enableZoom) {
			float scroll = Input.GetAxis ("Mouse ScrollWheel");
			_camera.orthographicSize += scroll;
			_camera.orthographicSize = Mathf.Clamp (_camera.orthographicSize, minZoom, maxZoom);
		}
	}

}
