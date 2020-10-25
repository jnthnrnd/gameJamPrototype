using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Keypads : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI displayText;

    private string codeSequence = "";
    bool firstPassed = false;
    bool secondPassed = false;
    bool thirdPassed = false;

    private void Start()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(1));
    }
    public void NumberClicked(int number)
    {
        if(codeSequence.Length < 3)
        {
            codeSequence += number.ToString();
            displayText.text = codeSequence;
        }
        displayText.color = Color.white;
    }

    public void DelNumber()
    {
        codeSequence = "";
        displayText.text = codeSequence;
        firstPassed = false;
        secondPassed = false;
        thirdPassed = false;
        displayText.color = Color.white;
    }

    //change the pass code below for each door
    public void EnterNumber()
    {
        for (int i = 0; i <= codeSequence.Length -1; i++)
        {
            if(codeSequence[i] == '5')
            {
                firstPassed = true;
            }
            if (codeSequence[i] == '1')
            {
                secondPassed = true;
            }

            if (codeSequence[i] == '9')
            {
                thirdPassed = true;
            }
        }
        if ((firstPassed && secondPassed) && thirdPassed)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(0));
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneByBuildIndex(1));
        }
        else
        {
            displayText.color = Color.red;
        }
    }
}
