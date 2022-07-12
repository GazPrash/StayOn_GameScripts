using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene4MMBlockGen : MonoBehaviour
{
    [SerializeField] 
    public GameObject BlockFloor;
    public float lowerSpawnBound, upperSpawnBound, BoundStart, BoundEnd, stableY;
    public float forwardFactor, storedFactor, blockLen;
    private int i = 0;


    private void Start() {
        StartCoroutine(GenBlocks());
    }

    IEnumerator GenBlocks(){
        while (true){
            if(transform.position.z <= BoundStart && transform.position.z >= BoundEnd){
                SpawnBlocks();
                forwardFactor += 1.25f;
            } else {
                forwardFactor = storedFactor;
                i = 0;
            }
            yield return new WaitForSeconds(0.55f);
        }
    }

    void SpawnBlocks(){
        var XPosn = Random.Range(lowerSpawnBound, upperSpawnBound);
        var Zposn = -10.5f - (Random.Range(0f, 1.85f)) - forwardFactor - (blockLen * i);
        i++;

        var new_floor = Instantiate(
            BlockFloor,
            new Vector3(XPosn, stableY, Zposn),
            BlockFloor.transform.rotation
        ) as GameObject;

        Destroy(new_floor, 2.5f);
        
    }
}
