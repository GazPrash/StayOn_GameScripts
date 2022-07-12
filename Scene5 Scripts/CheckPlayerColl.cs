using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CheckPlayerColl : MonoBehaviour
{
    [SerializeField] GameObject Player;
    private PlayerEntity pe;
    public bool PlayerOnFloor;
    public Renderer FloorRender;

    private void Start() {
        Player =  GameObject.Find("MainPlayer Variant V5");
        FloorRender = this.GetComponent<Renderer>();    
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject == Player) PlayerOnFloor = true;
    }

    private void OnCollisionExit(Collision other) {
        if ((other.gameObject == Player) && (VerifyPlayerBounds())){
            PlayerOnFloor = false;
        }
    }

    bool VerifyPlayerBounds(){
        if (Player.transform.position.x <= FloorRender.bounds.max.x){
            return false;
        }
        return true;
    }

    private void Update()
    {
        if (Player.transform.position.y <= -10f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }


}
