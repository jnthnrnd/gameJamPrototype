using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public GameObject mainMenu;
    public GameObject pauseMenu;
    public GameObject gameOverScreen;
    public GameObject playerCamera;
    public AudioMixer audioMixer;
    public bool isGameOver;
    public bool isPause = false;

    public static int doorNumber;

    // Update is called once per frame
    void Start()
    {
        isGameOver = true;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && !isGameOver)
        {
            PauseGame();
        }

        //handle scenes
        if (Input.GetKeyDown(KeyCode.F))
        {
            //first door
            if (PlayerControl.isDoor && doorNumber == 1)
            {
                //loads the DoorKeyCode_1 scene;
                SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
            }
        }

    }

    public void StartGame()
    {
        mainMenu.SetActive(false);
        isGameOver = false;
        playerCamera.SetActive(true);      
    }

    public void GameOver()
    {
        if (!isPause)
        {
            isGameOver = true;
            gameOverScreen.SetActive(true);
            scoreText.gameObject.SetActive(false);
        }
    }

    void PauseGame()
    {
        if (!isPause)
        {
            isPause = true;
            pauseMenu.SetActive(true);
            playerCamera.SetActive(false);
            //freeze time
            Time.timeScale = 0f;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void ResumeGame()
    {
        //what resume button does
        isPause = false;
        pauseMenu.SetActive(false);
        playerCamera.SetActive(true);
        Time.timeScale = 1f;
    }

    public void back()
    {
        if (isPause)
        {
            pauseMenu.SetActive(true);
        }
        else
        {
            mainMenu.SetActive(true);
        }
    }


    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
