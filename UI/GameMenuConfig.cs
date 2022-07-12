using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameMenuConfig : MonoBehaviour
{
    [SerializeField] GameObject Play, Level, Quit;
    string EntryScene = "MainMenuScene";
    string LevelSelector = "LevelSelector";

    private void Start()
    {
        Play.GetComponent<Button>().onClick.AddListener(PlayGame);
        Level.GetComponent<Button>().onClick.AddListener(SelectLevel);
        Quit.GetComponent<Button>().onClick.AddListener(QuitGame);

    }

    void PlayGame()
    {
        SceneManager.LoadScene(EntryScene);
    }

    void SelectLevel()
    {
        SceneManager.LoadScene(LevelSelector);
    }

    void QuitGame()
    {
        Application.Quit();
    }

}
