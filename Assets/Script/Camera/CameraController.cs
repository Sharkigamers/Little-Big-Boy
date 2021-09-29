using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    private Vector3 offsetPosition = new Vector3(0f, -2.5f, 10f);
    private Vector3 offsetRotation = new Vector3(10f, 0f, 0f);

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position - offsetPosition;
    }
}
