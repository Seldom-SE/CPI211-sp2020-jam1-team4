using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script to control the (bowling) ball's movement
/// 
/// Design Notes: Mouse movement is the only thing moving the camera
/// around the ball and the WASD is going to control rotation
/// </summary>
public class BallController : MonoBehaviour
{
    public GameObject ballObj;
    private Rigidbody _ballRigidBody
    {
        get
        {
            return ballObj.GetComponent<Rigidbody>();
        }
    }
    public GameObject cameraObj;

    public float movementSpeed = 1f;
    [SerializeField]
    private float _camAngle;
    [SerializeField]
    private float _lookSensitivity = 1f;

    private void Awake()
    {
        //Locks mouse cursor on screen so player does not see it
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        BallMovement();
        CameraControl();
    }

    /// <summary>
    /// Method that controls input for player movement
    /// </summary>
    private void BallMovement()
    {
        float hInput = Input.GetAxisRaw("Horizontal");
        float vInput = Input.GetAxisRaw("Vertical");

        if(hInput != 0)
        {

        }
    }

    private void CameraControl()
    {
        float mouseY = Input.GetAxis("Mouse Y");

        if(mouseY != 0)
        {
            _camAngle += mouseY * _lookSensitivity * Time.deltaTime;
        }

        if (Mathf.Abs(_camAngle) > 360)
            _camAngle %= 360;

        print(_camAngle);
    }
}
