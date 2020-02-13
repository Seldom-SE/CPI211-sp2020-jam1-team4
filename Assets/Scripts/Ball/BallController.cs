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
    [Header("Object References")]
    public GameObject ballObj;
    private Rigidbody _ballRigidbody
    {
        get
        {
            return ballObj.GetComponent<Rigidbody>();
        }
    }
    public GameObject camObj;
    /**
     * This is a gameobject that follows the ball's position. 
     * As a child, the camera is then able to follow the ball 
     * and rotating this obj will rotate the camera but not the ball
     */
    public GameObject ballFollower;
    public GameObject cameraObj;

    [Header("Movement")]
    public float movementSpeed = 1f;
    public float lookSensitivity = 1f;
    public float maxCamHeight;  //This restricts how high the camera cam be from the ball

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
    }

    /// <summary>
    /// I think late update is meant for cameras? I originally had this in fixed
    /// update but it felt kinda janky. Looked alot better here so am keeping it
    /// </summary>
    private void LateUpdate()
    {
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

        //Camera is always facing the ball with a 30 degree angle (I think)
        camObj.transform.LookAt(ballObj.transform, new Vector3(0, 30, 0));

        //Rotates the ball follower based on input. This allows the camera to rotate with the ball follower
        float mouseX = Input.GetAxis("Mouse X");
        ballFollower.transform.eulerAngles += new Vector3(0, lookSensitivity * mouseX, 0);

        //Moves the camera up and down between set constraints
        float mouseY = -Input.GetAxis("Mouse Y");
        Vector3 newPos = camObj.transform.position + new Vector3(0, lookSensitivity * mouseY * Time.deltaTime, 0);
        //Clams the y to not go below the ball follower or above the set max height
        newPos.y = Mathf.Clamp(newPos.y, ballFollower.transform.position.y, maxCamHeight);
        camObj.transform.position = newPos;

    }
}
