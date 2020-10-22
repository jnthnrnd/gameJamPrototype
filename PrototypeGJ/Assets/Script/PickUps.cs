using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUps : MonoBehaviour
{
    [SerializeField] GameObject scoreBox;
    [SerializeField] AudioSource pickUpSound;
    [SerializeField] int itemWorth;
    private void OnTriggerEnter()
    {
        Score.currentScore += itemWorth;
        //pickUpSound.Play();
        Destroy(gameObject);
    }
}
