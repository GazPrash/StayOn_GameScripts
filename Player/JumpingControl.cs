using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingControl : MonoBehaviour
{
    [SerializeField] public float JumpLimit, Gravity; 
    [System.NonSerialized] public float initial_y;

    private void Start() {
        initial_y = 25.037f;;
    }

    void Update()
    {
        if (this.transform.position.y >= JumpLimit + initial_y){
            jumpControl(); // write logic
        }
    }
    void jumpControl(){
        var gravity_force = new Vector3(0, -Gravity, 0);
        this.GetComponent<Rigidbody>().AddForce(gravity_force, ForceMode.Impulse);
    }
}
