using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPERotate : MonoBehaviour
{
    [SerializeField] private float RotationSpeed;

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0, Time.deltaTime * RotationSpeed, 0);
    }





}
