using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    [SerializeField]
    List<GameObject> AllButtons = new List<GameObject>();

    [System.NonSerialized]
    public Dictionary<GameObject, int> BtnMap = new Dictionary<GameObject, int> ();

    void Start()
    {
        var vec = new List<int>() { 0, 3, 4, 5, 6};
        int i = 0;
        foreach (GameObject btn in AllButtons)
        {
            btn.GetComponent<Button>().onClick.AddListener(delegate { SceneHandler(btn); });
            BtnMap[btn] = vec[i];
            i++;
        }

    }

    void SceneHandler(GameObject btn)
    {
        int scene_ind = BtnMap[btn];
        SceneManager.LoadScene(scene_ind);
    }

}
