using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    // Trend : All game defined player-related variables will start with a leading underscore.
    private Vector3 _inp;
    private Rigidbody _body;
    private float rotationFactor = 360f;
    public bool lockJump = false;
    public string[] solidGrounding = {"FloorBase", "MovingFloors", "SkipFloor"};
    [SerializeField] public float _speed, _jumpForce, GroundSeperation;
    [SerializeField] LayerMask GroundObject;

    void Start()
    {
        _body = GetComponent<Rigidbody> ();
    }

    void inputControl(){
        _inp = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    void handleMovement(){
        handleJumping();
        var _skewedInp = handleIsoTransformation();
        var newTransform =  transform.position + 
                            ((_skewedInp * _inp.normalized.magnitude)*
                            (_speed * Time.deltaTime));
        
        _body.MovePosition(newTransform);
    }

    void handleRotation(){
        if (_inp == Vector3.zero) return;

        var _skewedInp = handleIsoTransformation();
        var targetPOV = Quaternion.LookRotation(_skewedInp, Vector3.up);

        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetPOV,
            rotationFactor * Time.deltaTime
        );

    }

    public Vector3 handleIsoTransformation(){
        var skewedAngle = Quaternion.Euler(0, 45, 0);
        var isometricTransformation = Matrix4x4.Rotate(skewedAngle);
        Vector3 _skewedInp = isometricTransformation.MultiplyPoint3x4(_inp);
 
        return _skewedInp;

    }

    void handleJumping(){
        if (Input.GetKey(KeyCode.Space) && !lockJump){
            _body.AddForce(new Vector3(0, _jumpForce, 0), ForceMode.Impulse);
            lockJump = true;
        }

    }

    public void checkGrounding(){
        Collider[] objs = Physics.OverlapSphere(
            this.transform.position,
            GroundSeperation,
            GroundObject
        );

        lockJump = objs.Length == 0 ? true : false;

    }

    void Update()
    {
        inputControl();
        checkGrounding();
    }

    void FixedUpdate() {
        handleMovement();
        handleRotation();
        
    }
}
