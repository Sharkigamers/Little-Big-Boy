using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController characterController;

    float movementSpeed = 0.08f;

    float gravity = -12f;
    float jumpHeight = 3f;

    bool isGrounded;
    bool canDeep;
    bool canShallow;

    Vector3 velocity;

    float groundDistance = 0.1f;
    public Transform groundCheck;
    public LayerMask groundMask;

    public Transform deepCheck;
    public Transform shallowCheck;

    Vector3? toVerticallyMove = null;
    RaycastHit m_Hit;

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
        VerticalMovement();
        HorizontalMovement();
    }

    void HorizontalMovement() {
        float xMovement = Input.GetAxisRaw("Horizontal");

        Vector3 direction = new Vector3(xMovement, 0f, 0f).normalized;

        if (direction.magnitude >= 0.1 && toVerticallyMove == null)
            characterController.Move(direction * movementSpeed);
    }

    void VerticalMovement() {
        if (toVerticallyMove == null && Physics.CheckBox(new Vector3((float)(deepCheck.position.x - 0.4),
        deepCheck.position.y, deepCheck.position.z), new Vector3(0.1f, 0.1f, 0.1f), new Quaternion(), groundMask) &&
        Input.GetAxisRaw("Vertical") > 0 && isGrounded)
            toVerticallyMove = new Vector3(transform.position.x, transform.position.y, deepCheck.transform.position.z);
        else if (toVerticallyMove == null && Physics.CheckBox(new Vector3((float)(shallowCheck.position.x - 0.4),
        shallowCheck.position.y, shallowCheck.position.z), new Vector3(0.1f, 0.1f, 0.1f), new Quaternion(), groundMask)
        && Input.GetAxisRaw("Vertical") < 0 && isGrounded)
            toVerticallyMove = new Vector3(transform.position.x, transform.position.y, shallowCheck.transform.position.z);
        
        if (toVerticallyMove != null) {
            transform.position = Vector3.MoveTowards(transform.position, (Vector3)toVerticallyMove, 0.05f);
            if (Vector3.Distance((Vector3)toVerticallyMove, transform.position) == 0f)
                toVerticallyMove = null;
        }
    }

    void Gravity() {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        if (toVerticallyMove == null && Input.GetButtonDown("Jump") && isGrounded)
            velocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);

        if (isGrounded && velocity.y < 0f)
            velocity.y = -2f;
        
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(new Vector3((float)(deepCheck.position.x - 0.4), deepCheck.position.y, deepCheck.position.z), new Vector3(0.1f, 0.1f, 0.1f));
        Gizmos.DrawCube(new Vector3((float)(shallowCheck.position.x - 0.4), shallowCheck.position.y, shallowCheck.position.z), new Vector3(0.1f, 0.1f, 0.1f));
    }
}
