using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouseController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    float minCamZ = 5f;
    float maxCamZ = 15f;

    bool isDraggingCamera = false;
    Vector3 lastMousePosition;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDraggingCamera = true;

            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDraggingCamera = false;
        }

        if (isDraggingCamera)
        {
            Vector3 diff = lastMousePosition - Input.mousePosition;
            Camera.main.transform.Translate(diff * Time.deltaTime, Space.World);
            lastMousePosition = Input.mousePosition;
        }

        float scrollAmount = -Input.GetAxis("Mouse ScrollWheel") * 5f;
        float cameraSize = Camera.main.orthographicSize;
        if (Mathf.Abs(scrollAmount) > 0.01f && Mathf.Abs(cameraSize + scrollAmount) >= minCamZ && Mathf.Abs(cameraSize + scrollAmount) <= maxCamZ)
        {
            Camera.main.orthographicSize += scrollAmount;
        }

        // If there are two touches on the device...
        if (Input.touchCount == 2)
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
            Camera.main.orthographicSize += deltaMagnitudeDiff * 0.1f;
            Camera.main.orthographicSize = Mathf.Max(Camera.main.orthographicSize, minCamZ);

            // Make sure the orthographic size never goes above original size.
            Camera.main.orthographicSize = Mathf.Min(Camera.main.orthographicSize, maxCamZ);
        }

        cameraSize = Camera.main.orthographicSize;
        var cameraPosition = Camera.main.transform.position;
        if (cameraPosition.y < cameraSize - 1 )
        {
            Camera.main.transform.position = new Vector3(cameraPosition.x, cameraSize - 1, cameraPosition.z);
        }
        if (cameraPosition.y > (44.5f - cameraSize))
        {
            Camera.main.transform.position = new Vector3(cameraPosition.x, (44.5f - cameraSize), cameraPosition.z);
        }
    }
}
