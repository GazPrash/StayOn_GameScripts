using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCollison : MonoBehaviour
{
    private SphereCollider SoccerBallCollider;

    [SerializeField] private GameObject blockade1, blockade2;

    private void Start() {
        var sball = GameObject.FindGameObjectWithTag("SoccerBall");
        SoccerBallCollider = sball.gameObject.GetComponent<SphereCollider>();
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "SoccerBall"){
            var rotation = blockade1.transform.rotation;
            rotation.x += 90;
            blockade1.transform.rotation = rotation;
            blockade2.transform.rotation = rotation;
        }
    }

    private void FixedUpdate() {
        Physics.IgnoreCollision(SoccerBallCollider, this.gameObject.GetComponent<BoxCollider>());
    }
}
