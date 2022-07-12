using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anime;
    private Transform pTransform;

    public int AnimationState;

    void Start()
    {
        anime = GetComponent<Animator> ();
        var pObj = GameObject.FindGameObjectWithTag("Player");
        pTransform = pObj.GetComponent<Transform> ();
    }

    int setMotionState(){
        bool linearMove =   Input.GetKey("w") || 
                            Input.GetKey("a") ||
                            Input.GetKey("s") ||
                            Input.GetKey("d") ||
                            Input.GetKey(KeyCode.UpArrow) ||
                            Input.GetKey(KeyCode.LeftArrow) ||
                            Input.GetKey(KeyCode.RightArrow) ||
                            Input.GetKey(KeyCode.DownArrow);

        if (Input.GetKey(KeyCode.Space)){
            return 3;
        } else if (linearMove) return 2;

        return 1;

    }

    // Update is called once per frame
    void Update()
    {
        AnimationState = setMotionState();
        anime.SetInteger("State", AnimationState);
        // if (planarMovement()){
        //     anime.SetInteger("State", 2);
        // } else anime.SetInteger("State", 1);
    }
}
