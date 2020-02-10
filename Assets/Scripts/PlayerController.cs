﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script will control the player's movement which involves
/// moving the player as well as looking around with the player
/// 
/// Will most likely incorporate the ladder control here as well
/// </summary>
public class PlayerController : MonoBehaviour
{
    public Rigidbody Rigidbody
    {
        get
        {
            return GetComponent<Rigidbody>();
        }
    }

    [SerializeField]
    private GameObject _playerCam;

    [SerializeField]
    private float _lookSensitivity = 1f;
    [SerializeField]
    private float _maxVerticalAngle = 60f;
    public float movementSpeed = 1f;

    private void Awake()
    {
        //Locks mouse cursor on screen so player does not see it
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        MouseControl();
        MovementControl();
    }

    /// <summary>
    /// Method that handles mouse input and rotating the
    /// players view based on that output
    /// </summary>
    private void MouseControl()
    {
        //May not be a issue in the long run but I noticed that once an angular 
        //velocity is added the player would keep rotating so I added this to ensure it doesnt happen
        Rigidbody.angularVelocity = Vector3.zero;

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        //This moves the PLAYER gameobject's rotation
        if (mouseX != 0)
        {
            Vector3 newRotation = transform.eulerAngles;
            //I added * Time.deltaTime because it generally smoothes things out but wasnt sure if this was an appropriate case. Delete if not
            newRotation.y += mouseX * _lookSensitivity * Time.deltaTime;
            transform.eulerAngles = newRotation;
        }
        //This moves the CAMERA's rotation. This was done to avoid collision issues if the player obj were to rotate
        if (mouseY != 0)
        {
            Vector3 newRotation = _playerCam.transform.localEulerAngles;
            //I added * Time.deltaTime because it generally smoothes things out but wasnt sure if this was an appropriate case. Delete if not
            newRotation.x += mouseY * _lookSensitivity * -1 * Time.deltaTime;

            /**
             * This prevents the player from looking beyond our set _maxVerticalAngle;
             * 
             * Note: .localEulerAngles does not return a negative number as the inspector shows.
             * For example, -5 in the inspector is 355. This is the reason why I have to do two differnt
             * checks
             */
            if (newRotation.x < _maxVerticalAngle || newRotation.x > 360 - _maxVerticalAngle)
                _playerCam.transform.localEulerAngles = newRotation;
        }
    }

    /// <summary>
    /// Method that controls input for player movement
    /// </summary>
    private void MovementControl()
    {
        float hInput = Input.GetAxisRaw("Horizontal");
        float vInput = Input.GetAxisRaw("Vertical");

        if (hInput != 0)
        {
            transform.position += transform.right * Mathf.Sign(hInput) * movementSpeed * Time.deltaTime;
        }

        if (vInput != 0)
        {
            transform.position += transform.forward * Mathf.Sign(vInput) * movementSpeed * Time.deltaTime;
        }
    }
}