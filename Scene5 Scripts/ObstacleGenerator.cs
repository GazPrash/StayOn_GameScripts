using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    // this script must go on the player
    public GameObject MainFloor;
    [SerializeField] float lowerSpawnBound,
                           upperSpawnBound,
                           HiddenDepth,
                           forwardSpawnSpace, 
                           spawnDiffWidth, 
                           StableY, 
                           PopupTime, 
                           ObstacleDestroyTime,
                           StartTime,
                           RoundTime;
    [System.NonSerialized] public float TimeCounter = 0f;
    [SerializeField] GameObject Obstacle, EndLine;
    [SerializeField] GameObject Stage1;
    [SerializeField] ParticleSystem ShardApearanceEffect;

    private bool EndLineGen = false;


    void Start()
    {
        StartCoroutine(GameStart());
    }

    IEnumerator GameStart(){
        yield return new WaitForSeconds(StartTime);
        while(true){
            if (TimeCounter > 0.75f && !EndLineGen) {LoadEndLine();} 
            if (TimeCounter >= 15){GameAlterations();}
            SpawnObstacle();
            yield return new WaitForSeconds(RoundTime);
        }
    }

    void LoadEndLine(){
        var EL = Instantiate(
            EndLine,
            new Vector3(-3f,0f,3.369f),
            EndLine.transform.rotation
        );
        EL.GetComponent<EndLine> ().Player = this.gameObject;
        EndLineGen = true;

    }

    void GameAlterations(){

        if (TimeCounter < 25)
        {
            forwardSpawnSpace -= 2.5f;
        }
        else if (TimeCounter >= 25 && TimeCounter < 50)
        {
            forwardSpawnSpace -= 1.5f;
            RoundTime = 2f;
        }
        else if (TimeCounter >= 150)
        {
            forwardSpawnSpace = 4.75f;
        }


    }

    void SpawnObstacle(){
        var z_posn = Random.Range(lowerSpawnBound, upperSpawnBound);
        var playerPosn = this.transform.position.x;
        var x_posn = Random.Range(playerPosn+(spawnDiffWidth), playerPosn+forwardSpawnSpace);

        var new_obstacle = Instantiate(
            Obstacle,
            new Vector3(x_posn, StableY-HiddenDepth, z_posn),
            Quaternion.identity
        );

        var PopupPosn = new Vector3(x_posn, StableY, z_posn);
        StartCoroutine(MoveObstacle(
            new_obstacle.transform,
            PopupPosn,
            PopupTime // ->2.5s
        ));

        Destroy(new_obstacle, ObstacleDestroyTime);
    }

    public IEnumerator MoveObstacle(Transform curPosn, Vector3 newPosn, float movingTime)
    {
        float t = 0f;
        Vector3 start = curPosn.position;
        Vector3 v = newPosn - start;
        
        while(t < movingTime)
        {
            t += Time.deltaTime;
            curPosn.position = start + (v*t)/ movingTime;
            yield return null;
        }
        curPosn.position = newPosn;
        PlayAppearEffect(newPosn);

    }

    void PlayAppearEffect(Vector3 obstaclePosn){
        var AppearEffect = Instantiate(
            ShardApearanceEffect,
            obstaclePosn,
            Quaternion.identity
        ) as ParticleSystem;

        AppearEffect.Play();
        Destroy(AppearEffect, AppearEffect.main.duration);
    }                                                                   

    void Update(){
        TimeCounter += Time.deltaTime;

    }

}
