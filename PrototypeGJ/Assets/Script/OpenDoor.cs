using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public GameObject door;
    bool isOpen = false;
    
    void OnTriggerEnter(Collider other)
    {
        if (isOpen == false)
        {
            isOpen = true;
            door.transform.position += new Vector3(0, 0, -5);
        }
        
    }
}
