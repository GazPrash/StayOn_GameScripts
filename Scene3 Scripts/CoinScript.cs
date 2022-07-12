using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public float RotationSpeed;
    [SerializeField] ParticleSystem CoinDestroyEffect;

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Player"){
            var Effect = Instantiate(
                CoinDestroyEffect,
                this.transform.position,
                Quaternion.identity
            ) as ParticleSystem;
            Effect.Play();
            Destroy(this.gameObject);
            Destroy(Effect, Effect.main.duration);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.Rotate(Time.deltaTime * RotationSpeed, 0, 0);
    }
    
}
