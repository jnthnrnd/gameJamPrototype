using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    public GameObject interactMessage;
    public GameObject objectHolder;

    private float turnSmoothTime = 0.1f;
    private Vector3 direction = Vector3.zero;

    float turnSmoothVelocity;
    Vector3 velocity;
    bool isOnGround;
    bool hasObject = false;
    bool holdObject = false;
    bool dropObject = false;
    bool isLightOn = true;
    bool isSwitch = false;

    GameObject message;
    GameObject thing;

    public static bool isDoor = false;


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

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (hasObject)
            {
                holdObject = true;
                if (holdObject && !dropObject)
                {
                    Hold();
                }else if(holdObject && dropObject)
                {
                    Drop();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            if (isSwitch)
            {
                SwitchLights();
            }
        }

        if(!hasObject)
        {
            holdObject = false;
            dropObject = false;
        }
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

    void Hold()
    {
        dropObject = true;
        if (message != null)
        {
            Destroy(message);
        }
        RaycastHit hit;
        Ray directionRay = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(directionRay, out hit, 2f))
        {
                thing = hit.collider.gameObject;
                thing.transform.SetParent(objectHolder.transform);
                thing.transform.position = objectHolder.transform.position;
                thing.GetComponent<Rigidbody>().isKinematic = true;
                thing.GetComponent<Rigidbody>().useGravity = false;
        }
        moveSpeed -= thing.GetComponent<Rigidbody>().mass *2f;
    }

    void Drop()
    {
        objectHolder.transform.DetachChildren();
        thing.GetComponent<Rigidbody>().isKinematic = false;
        thing.GetComponent<Rigidbody>().useGravity = true;
        thing.GetComponent<Rigidbody>().AddForce(transform.forward * 2f, ForceMode.Impulse);
        holdObject = false;
        dropObject = false;
        moveSpeed += thing.GetComponent<Rigidbody>().mass*2f;
    }

    void SwitchLights()
    {
        GameObject lights = GameObject.FindGameObjectWithTag("Ceiling Light");
        Light[] bulbs = lights.GetComponentsInChildren<Light>();
        foreach (Light bulb in bulbs)
        {
            if (isLightOn)
            {
                //switch OFF
                bulb.enabled = false;
            }
            if (!isLightOn)
            {
                //switch ON
                bulb.enabled = true;
            }
        }

        if (isLightOn)
        {
            //switch OFF
            isLightOn = false;
        }
        else
        {   //switch ON
            isLightOn = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {

        string objectName = other.name;
        switch (objectName)
        {
            case "Switch":
                isSwitch = true;
                message = Instantiate(interactMessage, other.GetComponentInChildren<Light>().transform.position, Quaternion.Euler(0f,180f,0f));
                message.transform.SetParent(other.transform);
                message.GetComponent<TextMeshPro>().text = "Press 'G' to switch On/Off Lights";
                break;
            case "Lockers":
                hasObject = true;
                if (!holdObject && !dropObject)
                {
                    Vector3 messagePos = other.transform.position + interactMessage.transform.position;
                    message = Instantiate(interactMessage, messagePos, other.transform.rotation);
                    message.transform.SetParent(other.transform);
                    message.GetComponent<TextMeshPro>().text = "Press 'F' to Grab/Drop";
                }
                break;
            case "Door_1":
                isDoor = true;
                GameManager.doorNumber = 1;
                message = Instantiate(interactMessage, other.transform.position + new Vector3(0f,3,-1.5f), Quaternion.Euler(0f, 90f, 0f));
                message.transform.SetParent(other.transform);
                message.GetComponent<TextMeshPro>().text = "Press 'F' to Interact";
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Destroy(message, 0.5f);
        hasObject = false;
        isSwitch = false;
    }
}
