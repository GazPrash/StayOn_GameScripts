using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColorPartyScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject downBlock;
    public GameObject[] FloorList;
    public int floorCount = 8;

    public Dictionary<int, GameObject> floorNumberDict;
    public Dictionary<int, string> floorColorRefer;
    public Color displayColor;
    public Renderer ColorBoardRenderer;


    [SerializeField] public float roundTime, playerOriginal, stableY, extractTime;
    [SerializeField]public GameObject GameCoin;
    [SerializeField] float CoinDestructTime, CoinDropInterval;
    [SerializeField] GameObject Title;

    public bool CoinTaken = false;
    public float TimeCounter = 0f;


    void Start()
    {
        FloorList = GameObject.FindGameObjectsWithTag("ColorPartyFloors");
        var Cb = GameObject.FindGameObjectWithTag("ColorBoard");
        ColorBoardRenderer = Cb.GetComponent<Renderer> ();
        floorIndexSetup();
        floorColorSetup();

        // StartCoroutine(gameStart());
        GamePreps();

    }

    public void GamePreps(){
        StartCoroutine(gameStart());
        StartCoroutine(randomCoinGen());
    }

    public void floorIndexSetup(){
        floorNumberDict = new Dictionary<int, GameObject> ();
        foreach (GameObject gobj in FloorList){
            var ID = gobj.GetComponent<floorIdentity> ();
            floorNumberDict[ID.floorId] = gobj;
        }
    }

    public void floorColorSetup(){
        floorColorRefer = new Dictionary<int, string> (){
            {0,"#FF49EE"},
            {1,"#2FC5FF"},
            {2,"#FF4959"},
            {3,"#6BFF49"},
            {4,"#B849FF"},
            {5,"#FFF949"},
            {6,"#49FFCF"},
        };
    }

    IEnumerator gameStart(){
        // game count to 3 wait time
        yield return new WaitForSeconds(2f);

        while (true){
            if (TimeCounter > 10f)
            {
                GameAlterations();
            }

            var depreFloor = PickFloor();
            yield return new WaitForSeconds(0.75f);
            var curPosn = depreFloor.transform.position;
            StartCoroutine(MoveFloor(depreFloor.transform, new Vector3(curPosn.x, curPosn.y-10, curPosn.z), roundTime));
            yield return new WaitForSeconds(roundTime);
            StartCoroutine(MoveFloor(depreFloor.transform, new Vector3(curPosn.x, stableY, curPosn.z), extractTime));
            yield return new WaitForSeconds(1f);
            resetColorBoard();
        }
    }

    GameObject PickFloor(){
        int randFloorInd = Random.Range(0, 6);
        var hexcolor = floorColorRefer[randFloorInd];
        changeBoardColor(hexcolor);

        return floorNumberDict[randFloorInd];

    }

    void changeBoardColor(string hexcolor){
        Title.SetActive(false);
        ColorUtility.TryParseHtmlString(hexcolor, out displayColor);
        ColorBoardRenderer.material.SetColor("_Color1_R", displayColor);
    }

    public IEnumerator MoveFloor(Transform curPosn, Vector3 newPosn, float movingTime)
    {
        float t = 0f;
        Vector3 start = curPosn.position;
        Vector3 v = newPosn - start;
        while(t < movingTime)
        {
            t += Time.deltaTime;
            curPosn.position = start + v * t / movingTime;
            yield return null;
        }
    
        curPosn.position = newPosn;
    }

    void GameAlterations()
    {   
        //default values 
        //Round Time -> 2f && Extract Time -> 0.2f

        if (TimeCounter <= 25f)
        {
            roundTime = 1f;
        } 
        else if (roundTime > 25f)
        {
            roundTime = 0.75f;
            extractTime = 0.1f;
        }
    }

    void resetColorBoard(){
        string black = "#000000";
        changeBoardColor(black);
        Title.SetActive(true);

    }

    public IEnumerator randomCoinGen(){
        while (true){
            int randFloorInd = Random.Range(0, 6);
            var floor = floorNumberDict[randFloorInd];
            var floorPosn = floor.transform.position;
            var FloorBounds = floor.GetComponent<Renderer> ().bounds.size;
            var CoinBounds = GameCoin.GetComponent<Renderer> ().bounds.size;

            float coinY = 27.415f;
            float newRandX = Random.Range(-6.5f, 4.5f);
            float newRandZ = Random.Range(-5.5f, -15f);

            var newPosn = new Vector3(
                newRandX,
                coinY,
                newRandZ
            );

            var new_coin = Instantiate(
                GameCoin,
                newPosn,
                GameCoin.transform.rotation
            ) as GameObject;

            Destroy(new_coin, CoinDestructTime);
            yield return new WaitForSeconds(CoinDropInterval);
        }
    }

    private void Update() {
        if (Input.GetKey(KeyCode.R) ||
           (this.transform.position.y <= playerOriginal)){
            Reload();
        }

        TimeCounter += Time.deltaTime;
    }

    void Reload(){
        CPScore.GameOn = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
