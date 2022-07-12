using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScoreManager
{
    [SerializeField] private string HighScoreSuffix;

    public float LoadHighScore(string SceneName)
    {
        var Field = SceneName + HighScoreSuffix;

        if (PlayerPrefs.HasKey(Field))
        {
            return PlayerPrefs.GetFloat(Field);
        }

        return 0f;

    }

    public void UploadScore(string SceneName, float NewScore)
    {
        string HiScoreField = SceneName + HighScoreSuffix;

        if (!PlayerPrefs.HasKey(HiScoreField))
        {
            Debug.Log("New HiScore!");
            PlayerPrefs.SetFloat(HiScoreField, NewScore);
            return;
        }

        float hiScore = PlayerPrefs.GetFloat(
            HiScoreField
        );

        if (NewScore > hiScore)
        {
            PlayerPrefs.SetFloat(HiScoreField, NewScore);
            return;
        }

        string Field = SceneName;
        for (int i = 1; i <= 10; i++)
        {
            if (!PlayerPrefs.HasKey(Field + i.ToString()))
            {
                PlayerPrefs.SetFloat(Field + i.ToString(), NewScore);
                return;
            }

        }
        PlayerPrefs.SetFloat(Field + 10, NewScore);

    }


}
