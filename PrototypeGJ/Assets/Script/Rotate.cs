using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 2;
    private void Update()
    {
        transform.Rotate(0, rotateSpeed, 0, Space.World);
    }
}
