using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuState : MonoBehaviour
{
    [SerializeField] GameObject Resume, Level, Menu;
    [SerializeField] AudioClip MainMenuClip;

    void Start()
    {
        Resume.GetComponent<Button>().onClick.AddListener(ResumeGame);
        Level.GetComponent<Button>().onClick.AddListener(ChangeLevel);
        Menu.GetComponent<Button>().onClick.AddListener(ExitToMenu);
    }

    void ResumeGame()
    {
        PauseGame.GamePaused = false;
    }

    void ChangeLevel()
    {
        SceneManager.LoadScene("LevelSelector");
    }

    void ExitToMenu()
    {
        Time.timeScale = 1;
        PauseGame.GamePaused = false;
        string mainmenu = "GameMenu";
        SceneManager.LoadScene(mainmenu);
    }

}
