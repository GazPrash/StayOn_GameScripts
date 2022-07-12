using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SFScore : MonoBehaviour
{

    [SerializeField] public GameObject ScoreText, HiScoreText;
    [SerializeField] private int TileScore;
    public static bool GameOn = true;
    private ScoreManager SManage;
    private TMPro.TextMeshProUGUI ScoreTextElement, HiScoreTextElement;

    private void Start()
    {
        ScoreTextElement = ScoreText.GetComponent<TMPro.TextMeshProUGUI>();
        HiScoreTextElement = HiScoreText.GetComponent<TMPro.TextMeshProUGUI>();

        SManage = new ScoreManager();
        float HS = SManage.LoadHighScore("SkippingStones");
        HiScoreTextElement.text = "Current High Score : " + HS.ToString();
    }

    private void Update()
    {

        if (!GameOn)
        {
            ScoreReset();
        }

        ScoreTextElement.text = "SCORE : " + TileScore.ToString();
    }

    void ScoreReset()
    {
        UploadGameScore();
        GameOn = true;
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "SkipFloor"){
            TileScore++;
        }
        if (collision.gameObject.tag ==  "GameCoin")
        {
            TileScore += 10;
        }
    }

    public void UploadGameScore(){
        string Scenename = SceneManager.GetActiveScene().name;
        SManage.UploadScore(Scenename, TileScore);

    }



}
