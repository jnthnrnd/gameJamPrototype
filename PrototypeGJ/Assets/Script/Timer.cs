using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] GameObject timerBorder;
    [SerializeField] GameObject timerDisplay;
    [SerializeField] int theSecounds = 200;
    bool isTiming = false;
    

    // Update is called once per frame
    void Update()
    {
        if (isTiming == false)
        {
            StartCoroutine(SubtractTheSceounds());
        }
         
    }
    IEnumerator SubtractTheSceounds()
    {
        isTiming = true;
        theSecounds -= 1;
        timerBorder.GetComponent<Text>().text = "" + ":" + theSecounds;
        timerDisplay.GetComponent<Text>().text = "" + ":" + theSecounds;
        yield return new WaitForSeconds(1);
        isTiming = false;
    }
}
