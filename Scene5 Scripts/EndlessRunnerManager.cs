using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndlessRunnerManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public GameObject Stage1, Stage2, Stage3, Stage4, StageBlank, Stage0;
    public float PlatformLen, DestroyTime, NewSpawnTime;
    public int PlatformDistCount = 1;
    
    [SerializeField]
    public float OverlapRadius;

    [System.NonSerialized] public int lastSceneIndex = 5;
    [System.NonSerialized] public Dictionary<int, GameObject> SceneDict, SceneDictImmutable;
    [System.NonSerialized] public GameObject LastPlatform;

    [System.NonSerialized] public float StartScore, EndScore;

    void Start()
    {
        SceneDict = new Dictionary<int, GameObject>(){
            {1, Stage1},
            {2, Stage2},
            {3, Stage3},
            {4, Stage4},
            {5, StageBlank}
        };
        SceneDictImmutable = new Dictionary<int, GameObject> ();
        LastPlatform = GameObject.Find("StageInitial");

        StartCoroutine(GameStart());

    }

    IEnumerator GameStart(){
        LoadPlatforms();
        StartScore = this.transform.position.x;
        while (true){
            if (
                Mathf.Abs(this.transform.position.x - 
                (PlatformDistCount * PlatformLen)) < 42.5f
            ){
                TravelPlatform();
            }
            yield return null;
        }
    }

    void LoadPlatforms(){
        for (int i = 1; i <= 5; i++){
            var spawn_transform = SceneDict[i].transform.position;
            var platform = Instantiate(
                SceneDict[i],
                spawn_transform,
                Quaternion.identity
            ) as GameObject;
            platform.SetActive(false);
            // platform.GetComponentInChildren<CheckPlayerColl> ().PlayerOnFloor = this.gameObject;
            SceneDictImmutable[i] = SceneDict[i];
            SceneDict[i] = platform;
        }

    }

    void TravelPlatform(){
        var Index = SelectSceneRandom();
        var NextPlatform = SceneDict[Index];

        if (CheckPlatformColl(NextPlatform) && NextPlatform.activeSelf){
            initiateBackupScene();
            return;
        }

        var XIncrease = PlatformDistCount * PlatformLen;
        var spawn_transform = SceneDictImmutable[Index].transform.position;

        var NextPosn = new Vector3(
            spawn_transform.x + XIncrease,
            spawn_transform.y,
            spawn_transform.z
        );
        NextPlatform.transform.position = NextPosn;
        NextPlatform.SetActive(true);
        LastPlatform = NextPlatform;
        PlatformDistCount++;

    }

    bool CheckPlatformColl(GameObject AnonyStage){
        var SceneFloor = AnonyStage.transform.GetChild(0).gameObject;
        return SceneFloor.GetComponent<CheckPlayerColl>().PlayerOnFloor;
    }

    int SelectSceneRandom(){
        var chance = Random.Range(0f, 1f);
        // 0-0.1 0.1-0.2 0.2-0.3 0.3-0.4 0.4-0.5 0.5-1f
        int SceneIndex;

        if (chance <= 0.1f){
            SceneIndex = 1;
        } else if (chance > 0.1f && chance <=0.2f){
            SceneIndex = 2;
        } else if (chance >0.2f && chance <=0.3f){
            SceneIndex = 3;
        } else if (chance >0.3f && chance <= 0.4f){
            SceneIndex = 4;
        } else SceneIndex = 5;

        if (SceneIndex == lastSceneIndex){
            SceneIndex += Random.Range(-1, 2);
            SceneIndex = SceneIndex !=5 ? SceneIndex%5:5;
        }
        
        lastSceneIndex = SceneIndex;
        return SceneIndex;

    }


    void initiateBackupScene(){
        var spawn_transform = Stage0.transform.position;
        var XIncrease = PlatformDistCount * PlatformLen;
        
        var platform = Instantiate(
            Stage0,
            new Vector3(
                spawn_transform.x + XIncrease,
                spawn_transform.y,
                spawn_transform.z
            ),
            Quaternion.identity
        ) as GameObject;
        PlatformDistCount++;

    }

    public void ReloadLevel()
    {
        EndScore = this.transform.position.x;
        ScoreDR.GameOn = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
