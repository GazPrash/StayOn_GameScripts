using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiscGame2 : MonoBehaviour
{
    [SerializeField] GameObject Player;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == Player)
        {
            Player.GetComponent<Hurdles>().ReloadLevel();
        }
    }

}
