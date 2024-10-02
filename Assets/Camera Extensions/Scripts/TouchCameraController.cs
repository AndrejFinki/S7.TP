using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CameraBounds))]
public class TouchCameraController : MonoBehaviour
{
	public float orthoZoomSpeed = 0.05f;
	public bool enableZoom = true;
	public float minZoom = 1.0f;
	public float maxZoom = 20.0f;
	public bool invertMoveX = false;
	public bool invertMoveY = false;
	public bool lockX = false;
	public bool lockY = false;
	public float inertiaDuration = 1.0f;

	private Camera _camera;
	private float scrollVelocity = 0.0f;
	private float timeTouchPhaseEnded;
	private Vector3 scrollDirection;
	private Vector2 prevTouchPosition;
	private int singleTouchID = -1;

	void Start ()
	{
		_camera = GetComponent<Camera> ();
		CameraBounds cameraBounds = GetComponent<CameraBounds> ();

		float mapWidth = cameraBounds.mapMaxX - cameraBounds.mapMinX;
		float mapHeight = cameraBounds.mapMaxY - cameraBounds.mapMinY;

		maxZoom = 0.5f * (mapWidth / _camera.aspect);
		
		if (mapWidth > mapHeight)
			maxZoom = 0.5f * mapHeight;
		
		if (_camera.orthographicSize > maxZoom)
			_camera.orthographicSize = maxZoom;
	}

	void Update ()
	{
		
		Touch[] touches = Input.touches;
		
		if (touches.Length < 1) {
			//if the camera is currently scrolling
			if (scrollVelocity != 0.0f) {
				//slow down over time
				float t = (Time.time - timeTouchPhaseEnded) / inertiaDuration;
				float frameVelocity = Mathf.Lerp (scrollVelocity, 0.0f, t);

				Vector3 delta = scrollDirection * frameVelocity * Time.deltaTime;

				float modifierX = lockX ? 0 : 1;
				modifierX = !invertMoveX ? modifierX : modifierX * -1;
				float modifierY = lockY ? 0 : 1;
				modifierY = !invertMoveY ? modifierY : modifierY * -1;
				
				delta.Scale (new Vector3 (modifierX, modifierY, 0));
				
				_camera.transform.position += delta;

				if (t >= 1.0f)
					scrollVelocity = 0.0f;
			}
		}
		
		if (touches.Length > 0) {

			//Single touch (move)
			if (touches.Length == 1) {
				Touch touch = touches [0];
				if (touch.phase == TouchPhase.Began) {
					scrollVelocity = 0.0f;
					prevTouchPosition = touch.position;
					singleTouchID = touch.fingerId;
				} else if (touch.fingerId == singleTouchID) {
					if (touch.phase == TouchPhase.Moved) {
						Vector3 delta = _camera.ScreenToWorldPoint (touch.position) - _camera.ScreenToWorldPoint (prevTouchPosition);
						prevTouchPosition = touch.position;

						float modifierX = lockX ? 0 : 1;
						modifierX = invertMoveX ? modifierX : modifierX * -1;
						float modifierY = lockY ? 0 : 1;
						modifierY = invertMoveY ? modifierY : modifierY * -1;

						delta.Scale (new Vector3 (modifierX, modifierY, 0));

						_camera.transform.position += delta;
					
						scrollDirection = delta.normalized;
						scrollVelocity = delta.magnitude / Time.deltaTime;
					
						if (scrollVelocity <= 10)
							scrollVelocity = 0;
					} else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) {
						timeTouchPhaseEnded = Time.time;
						singleTouchID = -1;
					}
				}
			} else {
				singleTouchID = -1;
			}
			
			//Double touch (zoom)
			if (touches.Length == 2 && enableZoom) {
				Vector2 cameraViewsize = new Vector2 (_camera.pixelWidth, _camera.pixelHeight);
				
				Touch touchOne = touches [0];
				Touch touchTwo = touches [1];
				
				Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
				Vector2 touchTwoPrevPos = touchTwo.position - touchTwo.deltaPosition;
				
				float prevTouchDeltaMag = (touchOnePrevPos - touchTwoPrevPos).magnitude;
				float touchDeltaMag = (touchOne.position - touchTwo.position).magnitude;
				
				float deltaMagDiff = prevTouchDeltaMag - touchDeltaMag;

				Vector3 delta = _camera.transform.TransformDirection ((touchOnePrevPos + touchTwoPrevPos - cameraViewsize) * _camera.orthographicSize / cameraViewsize.y);
				float positionX = lockX ? 0 : delta.x;
				float positionY = lockY ? 0 : delta.y;
				_camera.transform.position += new Vector3 (positionX, positionY, 0);
				
				_camera.orthographicSize += deltaMagDiff * orthoZoomSpeed;
				_camera.orthographicSize = Mathf.Clamp (_camera.orthographicSize, minZoom, maxZoom) - 0.001f;

				delta = _camera.transform.TransformDirection ((touchOne.position + touchTwo.position - cameraViewsize) * _camera.orthographicSize / cameraViewsize.y);
				positionX = lockX ? 0 : delta.x;
				positionY = lockY ? 0 : delta.y;
				_camera.transform.position -= new Vector3 (positionX, positionY, 0);

			}
		}
	}


}
