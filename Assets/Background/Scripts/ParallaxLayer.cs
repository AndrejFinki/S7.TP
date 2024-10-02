using UnityEngine;
using System.Collections;

public class ParallaxLayer : MonoBehaviour
{
	public float speedX;
	public float speedY;
	private Transform cameraTransform;
	private Vector3? previousCameraPosition;

	void OnEnable ()
	{
		GameObject gameCamera = Camera.main.gameObject;
		cameraTransform = gameCamera.transform;
	}

	void LateUpdate ()
	{
		if (previousCameraPosition == null) {
			previousCameraPosition = cameraTransform.position;
			return;
		}

		Vector3 distance = cameraTransform.position - (Vector3) previousCameraPosition;
		float direction = -1;
		transform.position += Vector3.Scale (distance, new Vector3 (speedX, speedY)) * direction;
		previousCameraPosition = cameraTransform.position;
	}
}
