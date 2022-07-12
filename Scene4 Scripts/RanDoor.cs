using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RanDoor : MonoBehaviour
{

    [SerializeField] GameObject door1, door2, door3, 
                                door4, door5, door6, 
                                door7, door8, door9;

    [System.NonSerialized] GameObject[] DoorJunc1, DoorJunc2, DoorJunc3;

    void Start()
    {
        DoorJunc1 = new GameObject[3] {door1, door2, door3};
        DoorJunc2 = new GameObject[3] {door4, door5, door6};
        DoorJunc3 = new GameObject[3] {door7, door8, door9};

        DoorGame();
    }

    void DoorGame(){
        MakeOpenDoors(DoorJunc1);
        MakeOpenDoors(DoorJunc2);
        MakeOpenDoors(DoorJunc3);
    }

    void MakeOpenDoors(GameObject[] DoorJunc){
        var randoNum = Random.Range(1, 1001)%3;
        var OpenDoor = DoorJunc[randoNum];
        
        OpenDoor.AddComponent<Rigidbody> ();
        OpenDoor.GetComponent<MeshCollider> ().convex = true;
        // OpenDoor.AddComponent<BoxCollider> ();
    }


}
