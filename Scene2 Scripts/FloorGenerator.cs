using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FloorGenerator : MonoBehaviour
{
    public GameObject FloorPrefab;
    public int floorCount = 1, random_walk, last_walk = -1;
    public Vector3 floorDim;
    public delegate void AfterDelayDelegate();
    public AfterDelayDelegate after_call;    // make sure these after calls take no args
    [System.NonSerialized] public int totalFloors = 0, totalJumps = 0;

    [SerializeField] public float mutualSepr, destructBy, buildBy, overlapRadius;

    private GameObject indexFloor;
    private Vector3 indexFloorPosn;
    private Rigidbody PlayerBody;
    private float DiffTimer = 0f;
    [SerializeField] private float playerSphereRadius;

    [SerializeField] public GameObject GameCoin;
    public Renderer CoinRender;
    public float CoinDestroyTime, CoinDropInterval;


    void Start()
    {
        after_call = gameStart;
        StartCoroutine(timeDelay(2f, after_call));
    }

    public void gameStart(){
        indexFloor = GameObject.FindGameObjectWithTag("SkipFloor");
        indexFloorPosn = indexFloor.transform.position;
        floorDim = this.GetComponent<Renderer> ().bounds.size;
        CoinRender = GameCoin.GetComponent<Renderer> ();
        // initialSetup();
        PlayerBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody> ();
        StartCoroutine(spawnFloor());
    }

    IEnumerator timeDelay(float time, AfterDelayDelegate after_call){
        yield return new WaitForSeconds(time);
        after_call();
    }

    public Vector3 randomizeCoords(){

        if (indexFloor.transform.position.z >= 10){
            random_walk = Random.Range(1, 4);
        } else random_walk = 1;

        float ascendX = floorDim.x + mutualSepr;
        float ascendY = floorDim.z + mutualSepr;

        switch (random_walk){
            case 1:
                ascendX = 0;
                break;

            case 2:
                ascendY = 0;
                break;

            case 4:
                ascendY = -ascendY;
                ascendX = 0;
                break;

            case 3:
                ascendX = -ascendX;
                ascendY = 0;
                break;

        }

        Vector3 Coords = indexFloor.transform.position;
        var new_transform = new Vector3(
            Coords.x += ascendX,
            Coords.y,
            Coords.z += ascendY
        );

        return Coords;
    }

    void instanceFloor(){
        var newCoords = randomizeCoords();
        if (checkOccupiedSpace(newCoords) || !checkPlayerNearby(newCoords)){return;}

        var new_floor = Instantiate(
            FloorPrefab,
            newCoords,
            Quaternion.identity
        ) as GameObject;
        new_floor.tag = "SkipFloor";

        int coinChance = Random.Range(1, 11);
        if (coinChance == 10){
            StartCoroutine(instanceCoin(new_floor));
        }

        indexFloor = new_floor;
        totalFloors++;

        StartCoroutine(destroyProcess(new_floor));

    }

    IEnumerator instanceCoin(GameObject floor){
        var coinPosn = floor.transform.position;

        var new_coin = Instantiate(
            GameCoin,
            new Vector3(
                coinPosn.x - 1.32f,
                coinPosn.y + (CoinRender.bounds.size.y),
                coinPosn.z + 0.45f
            ),
            GameCoin.transform.rotation
        ) as GameObject;

        Destroy(new_coin, CoinDestroyTime);
        yield return new WaitForSeconds(CoinDropInterval);

    }

    bool checkOccupiedSpace(Vector3 Coords){
        Collider[] Intersection = Physics.OverlapSphere(Coords, overlapRadius);
        return Intersection.Length == 0 ? false : true;

    }

    bool checkPlayerNearby(Vector3 Coords){
        float PlayerDistance = Vector3.Distance(
            Coords,
            PlayerBody.transform.position
        );

        return PlayerDistance <= playerSphereRadius ? true : false;  // TODO --> Dont hardcode

    }

    IEnumerator spawnFloor(){
        while (true){
            if (DiffTimer >= 5 && DiffTimer < 10){
                if (checkPlayerInactive()) ReloadGame();
            }
            if (DiffTimer >= 7) LevelAlters();
            if (PlayerBody.transform.position.y <= 24){
                ReloadGame();
            }
            yield return new WaitForSeconds(buildBy);
            instanceFloor();
        }

    }

    bool checkPlayerInactive(){
        if (PlayerBody.transform.position.z <= -1){
            return true;
        }
        return false;
    }
    
    IEnumerator destroyProcess(GameObject floorInstance){
        yield return new WaitForSeconds(destructBy*3/4);
        var floorBody = floorInstance.GetComponent<Rigidbody> ();
        floorBody.useGravity = true;
        floorBody.isKinematic = false;
        Destroy(floorInstance, destructBy/4);
        totalFloors--;
    }

    void LevelAlters(){
        int mark = Mathf.RoundToInt(DiffTimer);

        switch (mark)
        {
            case 7:
                destructBy = 2.5f;
                break;

            case 15:
                CoinDropInterval = 3f;
                buildBy = 0.2f;
                break;

            case 30:
                CoinDropInterval = 1.75f;
                playerSphereRadius = 20f;
                break;
        }

    }

    private void Update() {
        DiffTimer += Time.deltaTime;
    }

    public void ReloadGame(){
        SFScore.GameOn = false;
        StartCoroutine(WaitForGameReset());
    }

    IEnumerator WaitForGameReset()
    {
        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
