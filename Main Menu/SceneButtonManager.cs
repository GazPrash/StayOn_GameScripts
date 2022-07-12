using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButtonManager : MonoBehaviour
{
    [SerializeField] public GameObject  Button1,
                                        Button2,
                                        Button3,
                                        Button4;

    public float ButtonFloat;
    public bool[] ButtonDowns = {false, false, false, false};

    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision other) {
        int ButtonInd = CheckWhichButton(other);
        LoadSceneWith(ButtonInd);

    }

    int CheckWhichButton(Collision other){
        GameObject floater;
        int ButtonInd = -1;
        if (other.gameObject == Button1 && !ButtonDowns[0]){
            floater = Button1;
            ButtonInd = 1;
            ButtonDowns[0] = true;
        }        
        else if (other.gameObject == Button2 && !ButtonDowns[1]){
            floater = Button2;
            ButtonInd = 2;
            ButtonDowns[1] = true;
        }
        else if (other.gameObject == Button3 && !ButtonDowns[2]){
            floater = Button3;
            ButtonInd = 3;
            ButtonDowns[2] = true;
        }
        else if (other.gameObject == Button4 && !ButtonDowns[3]){
            floater = Button4;
            ButtonInd = 4;
            ButtonDowns[3] = true;
        }
        else return 0;

        FloatButton(floater);
        return ButtonInd;

    }

    void FloatButton(GameObject Button){
        Button.transform.Translate(new Vector3(0, -ButtonFloat, 0));
    }

    void LoadSceneWith(int Index){
        if (Index == 0) return;

        string SceneName = "MainMenuScene";
        switch (Index){
            case (1):
                SceneName = "SkippingStones";
                break;            
            case (2):
                SceneName = "ColorParty";
                break;            
            case (3):
                SceneName = "Hurdles";
                break;            
            case (4):
                SceneName = "Deathrun";
                break;            
        }

        SceneManager.LoadScene(SceneName);

    }

}
