using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreDR : MonoBehaviour
{
    [SerializeField] GameObject HiScoreText;
    [SerializeField] GameObject ScoreText;
    
    public float TravelScore;
    public static bool GameOn = true;

    TMPro.TextMeshPro TextElement_HiSc;
    TMPro.TextMeshProUGUI TextElement_Sc;

    private ScoreManager Smanage;
    public string SceneName = "Deathrun";

    [SerializeField] private Vector3 StartingPoint, PrevLoc;

    // Start is called before the first frame update
    void Start()
    {
        TextElement_HiSc = HiScoreText.GetComponent<TMPro.TextMeshPro>();
        TextElement_Sc = ScoreText.GetComponent<TMPro.TextMeshProUGUI>();

        Smanage = new ScoreManager();
        float HS = Smanage.LoadHighScore(SceneName);
        string HSBest = "BEST : " + HS.ToString("F2") + " meters";
        TextElement_HiSc.text = HSBest;

        Smanage = new ScoreManager();
    }

    string FormatTime(float ftime)
    {
        if (ftime == 0f) return "No Current Best Run";

        int minutes = (int)ftime / 60;
        int seconds = (int)ftime - 60 * minutes;

        return ("BEST : " + minutes.ToString() + "m " + seconds.ToString() + "s");
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameOn) GameReset();

        TravelScore += Vector3.Distance(transform.position, PrevLoc);
        PrevLoc = transform.position;

        string UpdatedScore = "SCORE : " + TravelScore.ToString("F2") + "m";
        TextElement_Sc.text = UpdatedScore;
    }

    void GameReset()
    {
        UploadGameScore();
        TravelScore = 0f;
        PrevLoc = StartingPoint;
        GameOn = true;
    }

    void UploadGameScore()
    {
        string scenename = SceneName;
        Smanage.UploadScore(scenename, TravelScore);
    }

}
