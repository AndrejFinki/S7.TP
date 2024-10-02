using UnityEngine;
using System.Collections;

public class StandaloneCameraController : MonoBehaviour
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
    private Vector3 prevMousePosition;

    void Start()
    {
        Input.simulateMouseWithTouches = false;
        _camera = GetComponent<Camera>();
        CameraBounds cameraBounds = GetComponent<CameraBounds>();

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

    void Update()
    {


        if (!Input.GetMouseButton(0))
        {
            //if the camera is currently scrolling
            if (scrollVelocity != 0.0f)
            {
                //slow down over time
                float t = (Time.time - timeTouchPhaseEnded) / inertiaDuration;
                float frameVelocity = Mathf.Lerp(scrollVelocity, 0.0f, t);
                
                Vector3 delta = scrollDirection * frameVelocity * Time.deltaTime;
                
                float modifierX = lockX ? 0 : 1;
                modifierX = !invertMoveX ? modifierX : modifierX * -1;
                float modifierY = lockY ? 0 : 1;
                modifierY = !invertMoveY ? modifierY : modifierY * -1;
                
                delta.Scale(new Vector3(modifierX, modifierY, 0));
                
                _camera.transform.position += delta;
                
                if (t >= 1.0f)
                    scrollVelocity = 0.0f;
            }
        } else
        {
            if (Input.GetMouseButtonDown(0))
            {
                scrollVelocity = 0.0f;
            } else {
                if (!Input.GetMouseButtonUp(0))
                {
                    Vector3 delta = _camera.ScreenToWorldPoint(Input.mousePosition) - _camera.ScreenToWorldPoint(prevMousePosition);
                        
                    float modifierX = lockX ? 0 : 1;
                    modifierX = invertMoveX ? modifierX : modifierX * -1;
                    float modifierY = lockY ? 0 : 1;
                    modifierY = invertMoveY ? modifierY : modifierY * -1;
                        
                    delta.Scale(new Vector3(modifierX, modifierY, 0));
                        
                    _camera.transform.position += delta;
                        
                    scrollDirection = delta.normalized;
                    scrollVelocity = delta.magnitude / Time.deltaTime;
                        
                    if (scrollVelocity <= 10)
                        scrollVelocity = 0;
                } else {
                    timeTouchPhaseEnded = Time.time;
                }
            }
            prevMousePosition = Input.mousePosition;
        }

        if (enableZoom)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            _camera.orthographicSize += scroll;
            _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, minZoom, maxZoom);
        }
    }

}
