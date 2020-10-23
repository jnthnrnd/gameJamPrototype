using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f, jumpForce = 10f;

    [SerializeField]
    private float horizontalInput, verticalInput, gravityModifier;

    public Rigidbody playerRb;
    private BoxCollider playerBoxCollider;
    private bool isOnGround = true;


    // Start is called before the first frame update
    void Start()
    {
        //get player's rigidbody
        if(playerRb == null)
        {
            playerRb = GetComponent<Rigidbody>();
        }

        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        JumpPlayer();
    }

    void MovePlayer()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * verticalInput * moveSpeed * Time.deltaTime);
        transform.Translate(Vector3.right * horizontalInput * moveSpeed * Time.deltaTime);
    }

    void JumpPlayer()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            isOnGround = false;
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }
}
