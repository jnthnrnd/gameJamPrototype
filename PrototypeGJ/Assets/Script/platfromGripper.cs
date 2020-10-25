using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platfromGripper : MonoBehaviour
{
    [SerializeField] GameObject platfrom;
    [SerializeField] GameObject player;
    private void OnTriggerEnter(Collider other)
    {
        player.transform.parent = platfrom.transform;
    }
    private void OnTriggerExit(Collider other)
    {
        player.transform.parent = null;
    }
}
