using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBallGame : MonoBehaviour
{
    [SerializeField] public float   ZPosn1, ZPosn2, ZPosn3, 
                                    ZPosn4, StableY; 
    [SerializeField] private GameObject Player, Canon;
    [SerializeField] private float StartPoint, EndPoint, RoundTime, MoveForce;

    [System.NonSerialized] private List<GameObject> MovingCanons = new List<GameObject> ();

    [SerializeField] private ParticleSystem DestroyEffect;

    void Start()
    {
        StartCoroutine(GameStart());
    }

    IEnumerator GameStart()
    {
        while (Player.transform.position.x <= 45.5)
        {
            SpawnCanon();
            yield return new WaitForSeconds(RoundTime);
        }
    }

    void SpawnCanon()
    {
        int slot = Random.Range(0, 4);
        float Zposn = 0;

        switch (slot)
        {

            case (0):
                Zposn = ZPosn1;
                break;

            case (1):
                Zposn = ZPosn2;
                break;

            case (2):
                Zposn = ZPosn3;
                break;

            case (3):
                Zposn = ZPosn4;
                break;
        }

        var NewCanon = Instantiate(
                Canon,
                new Vector3(StartPoint, StableY, Zposn),
                Canon.transform.rotation
            );

        TravelCanon(NewCanon);

    }

    void TravelCanon(GameObject canon)
    {
        var cp_scr = canon.AddComponent<CanonPush>();
        cp_scr.Player = Player;
        var rb_canon = canon.AddComponent<Rigidbody>();
        rb_canon.AddForce(MoveForce, 0, 0);
        MovingCanons.Add(canon);
    }

    private void Update()
    {
        if (MovingCanons.Count > 0)
        {
            int Len = MovingCanons.Count;
            for (int i = 0; i < Len; i++)
            {
                var obj = MovingCanons[i];
                if (obj.transform.position.x < EndPoint)
                {
                    var Clone = obj;
                    MovingCanons.RemoveAt(i);
                    PlayEffect(Clone.transform);
                    Destroy(Clone);
                    Len--;

                }
            }
        }
    }

    void PlayEffect(Transform posn)
    {
        var Effect = Instantiate(
                DestroyEffect,
                posn.position,
                DestroyEffect.transform.rotation
            );

    }


}
