using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Hscore : MonoBehaviour
{
    [SerializeField] public float TimeScore;
    [SerializeField] private GameObject BonusCoin;
    public static bool GameOn = true;
    public ScoreManager SManager;

    [SerializeField] GameObject ScoreText;
    TMPro.TextMeshProUGUI TextElement_Score;

    private void Start()
    {
        SManager = new ScoreManager();
        TextElement_Score = ScoreText.GetComponent<TMPro.TextMeshProUGUI>();
    }

    void Update()
    {
        if (!GameOn)
        {
            GameReset();
            // TODO : Display time & highscore at the finish line.
        }

        TimeScore += Time.deltaTime;

        string UpdatedScore = "Time : " + Mathf.RoundToInt(TimeScore) + "s";
        TextElement_Score.text = UpdatedScore;
    }

    public void GameReset()
    {
        SaveScore(TimeScore);
        TimeScore = 0f;
        GameOn = true;
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject == BonusCoin){
            TimeScore -= 15f;
        }
    }

    private void SaveScore(float NewScore)
    {
        string Scenename = SceneManager.GetActiveScene().name;
        SManager.UploadScore(Scenename, NewScore);
    }

}
