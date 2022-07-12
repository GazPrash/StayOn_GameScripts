using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonPush : MonoBehaviour
{
    public GameObject Player;
    public float ForceExerted = -550f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == Player)
        {
            Player.GetComponent<Rigidbody>().AddForce(0, 0, ForceExerted);
        }
    }
}
