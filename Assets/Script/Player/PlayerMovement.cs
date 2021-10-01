﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController characterController;

    float movementSpeed = 0.1f;

    float gravity = -12f;
    float jumpHeight = 3f;

    bool isGrounded;

    Vector3 velocity;

    float groundDistance = 0.12f;
    public Transform groundCheck;
    public LayerMask groundMask;
    public LayerMask playerMask;

    bool isVerticallyMoving = false;
    Vector3 moveToPoint;
    Vector3 beforeMoveToPoint;
    Vector3 oldMoveToPoint;

    public float offset;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Gravity();
    }

    private void FixedUpdate() {
        if (!isVerticallyMoving)
            updateVerticalMoveStatus();
        else
            updateVerticalMovement();
        HorizontalMovement();
    }

    void HorizontalMovement() {
        float xMovement = Input.GetAxisRaw("Horizontal");

        Vector3 direction = new Vector3(xMovement, 0f, 0f).normalized;

        if (direction.magnitude >= 0.1)
            characterController.Move(direction * movementSpeed);
    }

    bool CanWalkOn(Transform checkObject) {
        return false;
    }

    void updateVerticalMoveStatus() {
        float zMovement = Input.GetAxisRaw("Vertical");
        bool positiveIndicator = Physics.CheckCapsule(
        new Vector3(transform.position.x, transform.position.y - 0.24f, transform.position.z + 1),
        new Vector3(transform.position.x, transform.position.y + 0.24f, transform.position.z + 1), 0.25f);
        bool negativeIndicator = Physics.CheckCapsule(
        new Vector3(transform.position.x, transform.position.y - 0.24f, transform.position.z - 1),
        new Vector3(transform.position.x, transform.position.y + 0.24f, transform.position.z - 1), 0.25f);

        if (!positiveIndicator && zMovement > 0) {
            isVerticallyMoving = true;
            moveToPoint = new Vector3(transform.position.x, transform.position.y, Mathf.Round((int)transform.position.z + 1));
            beforeMoveToPoint = new Vector3(transform.position.x, transform.position.y, Mathf.Round((int)transform.position.z));
        } else if (!negativeIndicator && zMovement < 0) {
            isVerticallyMoving = true;
            moveToPoint = new Vector3(transform.position.x, transform.position.y, Mathf.Round((int)transform.position.z - 1));
            beforeMoveToPoint = new Vector3(transform.position.x, transform.position.y, Mathf.Round((int)transform.position.z));
        }
    }

    void updateVerticalMovement() {
        if (!Physics.CheckCapsule(
        new Vector3(transform.position.x, transform.position.y - 0.24f, moveToPoint.z),
        new Vector3(transform.position.x, transform.position.y + 0.24f, moveToPoint.z), 0.25f))
            moveToPoint = new Vector3(transform.position.x, transform.position.y, moveToPoint.z);
        else if (Physics.CheckCapsule(
        new Vector3(transform.position.x, transform.position.y - 0.24f, moveToPoint.z),
        new Vector3(transform.position.x, transform.position.y + 0.24f, moveToPoint.z), 0.25f, playerMask))
            moveToPoint = new Vector3(transform.position.x, transform.position.y, moveToPoint.z);
        else
            moveToPoint = new Vector3(beforeMoveToPoint.x, beforeMoveToPoint.y, beforeMoveToPoint.z);
        if (Vector3.Distance(transform.position, moveToPoint) < 0.04)
            isVerticallyMoving = false;
        else
            characterController.Move((moveToPoint - transform.position).normalized * 0.08f);
    }

    void Gravity() {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);

        if (isGrounded && velocity.y < 0f)
            velocity.y = -2f;
        
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    static void DrawWireCapsule(Vector3 p1, Vector3 p2, float radius)
    {
        #if UNITY_EDITOR
        // Special case when both points are in the same position
        if (p1 == p2)
        {
            Gizmos.DrawWireSphere(p1, radius);
            return;
        }
        using (new UnityEditor.Handles.DrawingScope(Gizmos.color, Gizmos.matrix))
        {
            Quaternion p1Rotation = Quaternion.LookRotation(p1 - p2);
            Quaternion p2Rotation = Quaternion.LookRotation(p2 - p1);
            // Check if capsule direction is collinear to Vector.up
            float c = Vector3.Dot((p1 - p2).normalized, Vector3.up);
            if (c == 1f || c == -1f)
                // Fix rotation
                p2Rotation = Quaternion.Euler(p2Rotation.eulerAngles.x, p2Rotation.eulerAngles.y + 180f,
                p2Rotation.eulerAngles.z);
            // First side
            UnityEditor.Handles.DrawWireArc(p1, p1Rotation * Vector3.left,  p1Rotation * Vector3.down, 180f, radius);
            UnityEditor.Handles.DrawWireArc(p1, p1Rotation * Vector3.up,  p1Rotation * Vector3.left, 180f, radius);
            UnityEditor.Handles.DrawWireDisc(p1, (p2 - p1).normalized, radius);
            // Second side
            UnityEditor.Handles.DrawWireArc(p2, p2Rotation * Vector3.left,  p2Rotation * Vector3.down, 180f, radius);
            UnityEditor.Handles.DrawWireArc(p2, p2Rotation * Vector3.up,  p2Rotation * Vector3.left, 180f, radius);
            UnityEditor.Handles.DrawWireDisc(p2, (p1 - p2).normalized, radius);
            // Lines
            UnityEditor.Handles.DrawLine(p1 + p1Rotation * Vector3.down * radius,
            p2 + p2Rotation * Vector3.down * radius);
            UnityEditor.Handles.DrawLine(p1 + p1Rotation * Vector3.left * radius,
            p2 + p2Rotation * Vector3.right * radius);
            UnityEditor.Handles.DrawLine(p1 + p1Rotation * Vector3.up * radius,
            p2 + p2Rotation * Vector3.up * radius);
            UnityEditor.Handles.DrawLine(p1 + p1Rotation * Vector3.right * radius,
            p2 + p2Rotation * Vector3.left * radius);
        }
        #endif
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        DrawWireCapsule(new Vector3(transform.position.x, transform.position.y - 0.24f, transform.position.z + 1),
        new Vector3(transform.position.x, transform.position.y + 0.24f, transform.position.z + 1), 0.25f);
        DrawWireCapsule(new Vector3(transform.position.x, transform.position.y - 0.24f, transform.position.z - 1),
        new Vector3(transform.position.x, transform.position.y + 0.24f, transform.position.z - 1), 0.25f);
        Gizmos.color = Color.blue;
        DrawWireCapsule(moveToPoint, moveToPoint, 0.25f);
    }
}
