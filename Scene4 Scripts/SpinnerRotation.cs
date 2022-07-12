using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerRotation : MonoBehaviour
{
    [SerializeField] 
    public float RotationSpeed;

    // Update is called once per frame
    void Update()
    {
        var new_rot = transform.rotation.eulerAngles;
        new_rot.y += RotationSpeed * Time.deltaTime;
        new_rot.y %= 360f;

        transform.rotation = Quaternion.Euler(new_rot);
    }
}
