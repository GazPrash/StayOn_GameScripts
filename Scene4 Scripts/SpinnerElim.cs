using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerElim : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject Player;

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject == Player){
            Player.GetComponent<Hurdles> ().ReloadLevel();
        }
    }
}
