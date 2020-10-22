using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] GameObject scoreBorder;
    [SerializeField] GameObject scoreBox;
    public static int currentScore;
    [SerializeField] int score;

    // Update is called once per frame
    void Update()
    {
        score = currentScore;
        scoreBorder.GetComponent<Text>().text = "" + score;
        scoreBox.GetComponent<Text>().text = "" + score;
    }
}
