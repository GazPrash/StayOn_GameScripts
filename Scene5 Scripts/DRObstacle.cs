using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DRObstacle : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ShardObstacle")
        {
            GetComponent<EndlessRunnerManager> ().ReloadLevel();
        }
    }

}
