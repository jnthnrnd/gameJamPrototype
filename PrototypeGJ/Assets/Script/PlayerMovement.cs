using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController controller;

    [SerializeField] float speed = 15;
    [SerializeField] float gravity = -19;
    [SerializeField] float jumpHeight = 3;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    Vector3 velocity;
    bool isground;
    CharacterController motor;

    // Start is called before the first frame update
    void Start()
    {
        motor = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveMethod();
    }
    void MoveMethod()
    {
        isground = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        if (isground && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isground)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
