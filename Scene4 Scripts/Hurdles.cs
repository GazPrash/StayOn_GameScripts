using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hurdles : MonoBehaviour
{
    [SerializeField] GameObject FinishLine;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R)){
            ReloadLevel();
        }

        if (this.transform.position.y <= 18){
            ReloadLevel();
        }
        
    }

    public void ReloadLevel(){
        Hscore.GameOn = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == FinishLine)
        {
            ReloadLevel();
        }
    }

}
