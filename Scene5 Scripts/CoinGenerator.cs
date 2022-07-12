using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject GameCoin;

    [SerializeField]
    public float    lowerSpawnBound, upperSpawnBound, PlatformLen, 
                    GenTime, StableY, DestroyTime,
                    SeprDist;
    
    [System.NonSerialized]
    public int PlatformCount;

    void Start()
    {
        PlatformCount = this.GetComponent<EndlessRunnerManager>().PlatformDistCount;
        StartCoroutine(GenerateCoin());
    }

    IEnumerator GenerateCoin(){
        yield return new WaitForSeconds(5f);
        while (true){
            SpawnCoin();
            yield return new WaitForSeconds(GenTime);
        }

    }

    void SpawnCoin(){
        var XPosn = this.transform.position.x;
        XPosn += Random.Range(XPosn + SeprDist, (PlatformLen/2));
        var ZPosn = Random.Range(lowerSpawnBound, upperSpawnBound);
        
        var new_coin = Instantiate(
            GameCoin,
            new Vector3(XPosn, StableY, ZPosn),
            GameCoin.transform.rotation
        ) as GameObject;

        Destroy(new_coin, DestroyTime);
    }

}
