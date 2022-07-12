using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerFallWatch : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= 0f)
        {
            SceneManager.LoadScene(
                    SceneManager.GetActiveScene().buildIndex
                );
        }
    }
}
