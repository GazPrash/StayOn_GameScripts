using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CPScore : MonoBehaviour
{
    [SerializeField] GameObject GameCoin;
    [SerializeField] public int Score;
    [SerializeField] public GameObject ScoreText, HiScoreText;

    private TMPro.TextMeshProUGUI TextElement, HSTextElement;
    private ScoreManager SManager;

    public static bool GameOn = true;


    private void Start()
    {
        TextElement = ScoreText.GetComponent<TMPro.TextMeshProUGUI>();
        HSTextElement = HiScoreText.GetComponent<TMPro.TextMeshProUGUI>();

        SManager = new ScoreManager();
        float HS = SManager.LoadHighScore(SceneManager.GetActiveScene().name);
        HSTextElement.text = "High Score : " + HS.ToString();

    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "GameCoin") Score++;
    }

    private void Update()
    {
        if (!GameOn) GameReset();

        string UpdatedScore = "SCORE : " + Score.ToString();
        TextElement.text = UpdatedScore;
    }

    private void GameReset()
    {
        UploadGameScore();
        Score = 0;
        TextElement.text = "SCORE : 0";
        GameOn = true;

    }

    public void UploadGameScore()
    {
        string Scenename = SceneManager.GetActiveScene().name;
        SManager.UploadScore(Scenename, Score);

    }

}
