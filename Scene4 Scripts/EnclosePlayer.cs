using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnclosePlayer : MonoBehaviour
{
    // Reference -> X is Z && Y is X

    [SerializeField] public float BoundMin, BoundMax;
    [SerializeField] public bool JumpAvail = false;
    
    [SerializeField] GameObject JumpPowerUp;
    [SerializeField] ParticleSystem DisperseEffect, JPUEffect;

    Movement PMove;

    private void Start() {
        PMove = this.GetComponent<Movement> ();
        JumpAvail = false;

    }

    private void LateUpdate() {
        if (!JumpAvail){
            PMove.lockJump = true;
        }

        if (this.transform.position.x <= 16.5f || this.transform.position.x >= 29){
            var curPosn = this.transform.position;
            curPosn.z = Mathf.Clamp(curPosn.z, BoundMin, BoundMax);
            this.transform.position = curPosn;
        }

    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject == JumpPowerUp)
        {
            JumpAvail = true;

            var Effect = Instantiate(
                DisperseEffect,
                JumpPowerUp.transform.position,
                DisperseEffect.transform.rotation
            );

            Effect.Play();
            Destroy(JumpPowerUp);
            Destroy(JPUEffect);
        }
    }

}
