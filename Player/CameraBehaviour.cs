using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraBehaviour : MonoBehaviour
{
    // members related to camera movement
    private Transform followTarget;
    [SerializeField] float smoothing;

    [SerializeField] public float CameraForwardLash;

    void Start()
    {
        var playerObj = GameObject.FindGameObjectWithTag("Player");
        followTarget = playerObj.GetComponent<Transform> ();

    }

    private void LateUpdate() {
        var targetLoc = new Vector3(
            followTarget.position.x + CameraForwardLash,  // add 10-15f for camera to lash forward
            followTarget.position.y,
            followTarget.position.z
        );

        transform.position = Vector3.Lerp(transform.position, targetLoc, smoothing * Time.deltaTime);

    }

}
