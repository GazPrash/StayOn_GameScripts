using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurdlesPSManage : MonoBehaviour
{
    [SerializeField] GameObject Player;
    public float ClearanceFactor;

    private void Start() {
        StartCoroutine(FollowUp());
    }

    IEnumerator FollowUp(){
        while (true){
            var playerPosn = Player.transform.position;
            playerPosn.y += ClearanceFactor;
            this.transform.position = playerPosn;
            yield return new WaitForSeconds(2f);
        }
    }
}
