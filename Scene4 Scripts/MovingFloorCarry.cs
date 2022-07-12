using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFloorCarry : MonoBehaviour
{
    [SerializeField] 
    GameObject Player;

    [System.NonSerialized]
    public bool MovePlayer = false;
    public bool relapseOn = false;

    public float relapsePeriod = 0.25f;


    bool CheckMovement(){
        if (
            Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.D) ||
            Input.GetKey(KeyCode.Space)
        ) return true;

        return false;
    }

    private void Update() {
        Debug.Log(relapseOn);
        if (CheckMovement()){
            MovePlayer = false;
            Player.transform.parent = null;
            relapseOn = true;
            StartCoroutine(relpaseOffRoutine());
        } else if (MovePlayer) {Player.transform.parent = this.transform;}
        
    }

    IEnumerator relpaseOffRoutine(){
        yield return new WaitForSeconds(relapsePeriod);
        relapseOn = false;
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject == Player && !relapseOn){
            MovePlayer = true;
            Player.transform.parent = this.transform;
        }
    }

    private void OnCollisionExit(Collision other) {
        if (other.gameObject == Player){
            MovePlayer = false;
            Player.transform.parent = null;
        }
    }


}
