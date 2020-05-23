// CameraZoomPinch.cs
// Last edited 12:07 PM 06/23/2015 by Aaron Freedman

using UnityEngine;

public class CameraZoomPinch : MonoBehaviour
{
    public int speed = 4;
    public Camera selectedCamera;
    public float minPinchSpeed = 5.0F;
    public float varianceInDistances = 5.0F;
    private float touchDelta;
    private Vector2 prevDist = new Vector2(0, 0);
    private Vector2 curDist = new Vector2(0, 0);
    private float speedTouch0;
    private float speedTouch1;
    public int maxOrtho;
    public int minOrtho;

    private void Start() {}

    private void Update()
    {
        if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)
        {
            // early escapes
            speedTouch0 = Input.GetTouch(0).deltaPosition.magnitude / Input.GetTouch(0).deltaTime;
            speedTouch1 = Input.GetTouch(1).deltaPosition.magnitude / Input.GetTouch(1).deltaTime;
            bool pinchOk = (speedTouch0 > minPinchSpeed) && (speedTouch1 > minPinchSpeed);
            if (!pinchOk) return;


            curDist = Input.GetTouch(0).position - Input.GetTouch(1).position; //current distance between finger touches
            prevDist = ((Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition) -
                        (Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition));
            //difference in previous locations using delta positions
            touchDelta = curDist.magnitude - prevDist.magnitude;


            float t = touchDelta + varianceInDistances;

            if (selectedCamera.orthographic)
            {
                selectedCamera.orthographicSize = Mathf.Clamp(selectedCamera.orthographicSize + (speed * Mathf.Sign(t)), minOrtho, maxOrtho);
            }
            else
            {
                selectedCamera.fieldOfView = Mathf.Clamp(selectedCamera.fieldOfView + (speed * Mathf.Sign(t)), 15, 90);
            }
        }
    }
}