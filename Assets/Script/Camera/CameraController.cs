using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    private float smoothSpeed = 0.06f;

    private Vector3 offsetPosition = new Vector3(0f, 2.5f, -8.5f);

    // Start is called before the first frame update
    void Start()
    {
    }

    private void FixedUpdate() {
        Vector3 desiredPosition = target.position + offsetPosition;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
