using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 6f, jumpHeight = 3f, groundDistance = 0.4f;

    [SerializeField]
    private float horizontalInput, verticalInput, gravity = -9.81f;
    
    [SerializeField]
    private Transform groundCheck;

    [SerializeField]
    private LayerMask groundMask;

    public CharacterController controller;
    public Transform cam;

    private float turnSmoothTime = 0.1f;
    private Vector3 direction = Vector3.zero;

    float turnSmoothVelocity;
    Vector3 velocity;
    bool isOnGround;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        isOnGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isOnGround && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        MovePlayer();
        JumpPlayer();
      
    }

    void MovePlayer()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        direction = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            controller.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);
        }
    }

    void JumpPlayer()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
          velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Press F to Interact");

    }
}
