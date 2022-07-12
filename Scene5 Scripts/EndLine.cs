using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLine : MonoBehaviour
{
    public GameObject Player;
    private float tCtr = 0f;
    [SerializeField] public float MoveForce;

    private void Start() {
    }

    private void FixedUpdate()
    {
        var new_transform = transform.position;
        new_transform.x += MoveForce * Time.deltaTime;
        transform.position = new_transform;
    }

    private void Update() {
        tCtr += Time.deltaTime;
        if (tCtr >= 10){
            MoveForce = 8f; // or something like that ...
        } else if (tCtr >= 20){
            MoveForce = 8.75f;
        } else if (tCtr >= 30){
            MoveForce = 9.5f;
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject == Player){
            Player.GetComponent<EndlessRunnerManager> ().ReloadLevel();
        }
    }


}
