using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    [SerializeField] public GameObject PauseMenu;
    [System.NonSerialized] public static bool GamePaused = false;
    [System.NonSerialized] private GameObject ActivePauseMenu;


    private void Awake()
    {
        ActivePauseMenu = Instantiate(
            PauseMenu,
            Vector3.zero,
            PauseMenu.transform.rotation
        );
        ActivePauseMenu.SetActive(false);
    }

    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(PauseTriggerAction);
    }

    void EnablePauseMenu()
    {
        Time.timeScale = 0;
        ActivePauseMenu.SetActive(true);
    }

    void DisablePauseMenu()
    {
        ActivePauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    void PauseTriggerAction()
    {
        GamePaused = !GamePaused;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseTriggerAction();
        }

        if (GamePaused)
        {
            EnablePauseMenu();
        }
        else
        {
            DisablePauseMenu();
        }

    }
}
