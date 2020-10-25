using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Puzzles : MonoBehaviour
{
    void OnTriggerEnter()
    {
        SceneManager.LoadScene(1);
    }
}
