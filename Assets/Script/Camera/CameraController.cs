using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    float smoothSpeed = 0.06f;

    Camera camera;
    public float zoomSensitivity= 15.0f;
     public float zoomSpeed= 5.0f;
     public float zoomMin= 5.0f;
     public float zoomMax= 80.0f;
     
     private float zoom;

    Vector3 offsetPosition = new Vector3(0f, 1.5f, -5.5f);
    Vector3 offsetZoomPosition = new Vector3(0f, 0.25f, -1.5f);

    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
    }

    private void FixedUpdate() {
        Vector3 desiredPosition;
        if (Input.GetMouseButton(1))
            desiredPosition = target.position + offsetZoomPosition;
        else
            desiredPosition = target.position + offsetPosition;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

    private void LateUpdate() {
        zoom = Mathf.Clamp(zoom * zoomSensitivity, zoomMin, zoomMax);
        camera.fieldOfView = Mathf.Lerp (camera.fieldOfView, zoom, Time.deltaTime * zoomSpeed);
    }
}
