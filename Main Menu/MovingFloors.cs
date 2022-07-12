using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFloors : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject playerObj;
    
    void Awake()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other) {
        playerObj.transform.parent = transform;
    }

    private void OnTriggerExit(Collider other) {
        playerObj.transform.parent = null;
    }

    
}
