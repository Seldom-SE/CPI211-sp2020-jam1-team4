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
    private Rigidbody _ballRigidbody
    {
        get
        {
            return ballObj.GetComponent<Rigidbody>();
        }
    }
    /**
     * This is a gameobject that follows the ball's position. 
     * As a child, the camera is then able to follow the ball 
     * and rotating this obj will rotate the camera but not the ball
     */
    public GameObject ballFollower;
    public GameObject cameraObj;

    public float movementSpeed = 1f;
    public float lookSensitivity = 1f;

    private void Awake()
    {
        //Locks mouse cursor on screen so player does not see it
        Cursor.lockState = CursorLockMode.Locked;

        //Increases the max velocity as it's initially really low
        _ballRigidbody.maxAngularVelocity = movementSpeed;
    }

    private void FixedUpdate()
    {
        BallMovement();
        CameraControl();
    }

    /// <summary>
    /// Method that controls the balls movement based on player input
    /// </summary>
    private void BallMovement()
    {
        float hInput = Input.GetAxisRaw("Horizontal");
        float vInput = Input.GetAxisRaw("Vertical");

        //Note: Both use ballfollower.transform because the camera is based on that obj therefore the direction of the follower is (for the most part) the same as the camera
        if (hInput != 0)
            _ballRigidbody.angularVelocity += ballFollower.transform.right * movementSpeed * hInput;

        if(vInput != 0)
            _ballRigidbody.angularVelocity += ballFollower.transform.forward * movementSpeed * vInput;
    }

    /// <summary>
    /// Method that controls the camera to follow the ball and rotate based on player input
    /// </summary>
    private void CameraControl()
    {
        //This keeps the camera following the ball
        ballFollower.transform.position = ballObj.transform.position;

        //Rotates the ball follower based on input. This allows the camera to rotate with the ball follower
        float mouseX = Input.GetAxis("Mouse X");
        ballFollower.transform.eulerAngles += new Vector3(0, lookSensitivity * mouseX, 0);
    }
}
