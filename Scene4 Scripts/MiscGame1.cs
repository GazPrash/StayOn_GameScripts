using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiscGame1 : MonoBehaviour
{
    [SerializeField] public GameObject  board1, board2, board3,
                                        board4, board5, board6;

    public GameObject[] boardJunc1, boardJunc2;

    [System.NonSerialized] public GameObject lastDeactive1, lastDeactive2;

    void Start()
    {
        boardJunc1 = new GameObject[3] {board1, board2, board3};
        boardJunc2 = new GameObject[3] {board4, board5, board6};
        
        lastDeactive1 = board1;
        lastDeactive2 = board4;

        StartCoroutine(GameStart());
    }

    IEnumerator GameStart(){
        while (true){
            DisappearBoard();
            yield return new WaitForSeconds(0.8f);
        }

    }

    void DisappearBoard(){
        var randoNum1 = Random.Range(0, 1001)%3;
        var randoNum2 = Random.Range(1001, 2002)%3;

        lastDeactive1.gameObject.SetActive(true);
        lastDeactive2.gameObject.SetActive(true);

        lastDeactive1 =  boardJunc1[randoNum1];
        lastDeactive2 = boardJunc2[randoNum2];

        lastDeactive1.SetActive(false);
        lastDeactive2.SetActive(false);

    }

        
}
