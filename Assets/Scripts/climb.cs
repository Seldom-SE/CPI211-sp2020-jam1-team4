using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class climb : MonoBehaviour
{
    //init vars
    Transform player;
    GameObject playerObj;
    bool canClimb = true;
    float speed = 30;

    //entering the trigger collider
     void OnTriggerEnter(Collider other)
     {
        //if we can climb the ladder
        if (canClimb == true)
        {
            //update vars
            playerObj = other.gameObject;
            player = other.transform;

            //set player's climbing var to false to prevent x and z movement
            playerObj.GetComponent<PlayerController>().climbing = false;
            
            //stop applying gravity to the transform
            player.GetComponent<Rigidbody>().useGravity = false;

            //player can no longer start climbing because player is already on a ladder
            canClimb = false;
            return;
        }

        //if we cannot climb a ladder (presumably because we are already climbing one)
        else//if (canClimb == false)
        {
            
            canClimb = true;
            player.GetComponent<Rigidbody>().useGravity = true;
            player = null;
            playerObj = null;
        }

    }

    void OnTriggerStay(Collider other)
    {
        //update vars
        player = other.transform;
        playerObj = other.gameObject;
        //if we're climbing, halt player xz position and gravity
        if (canClimb == false)
        {
            playerObj.GetComponent<PlayerController>().climbing = true;
            playerObj.GetComponent<Rigidbody>().useGravity = false;
            playerObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
        }

        //go up or down
        if (Input.GetKey(KeyCode.W))
        {
            player.Translate(Vector3.up * Time.deltaTime * speed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            player.Translate(Vector3.down * Time.deltaTime * speed);
        }

    }
    
    //push player off of the ladder
    void OnTriggerExit(Collider other)
    {
        playerObj = other.gameObject;
        player = other.transform;

        //remove constraints and set player climbing to false because they are no longer climbing
        playerObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        playerObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;

        playerObj.GetComponent<PlayerController>().climbing = false;
        
        //gravity exists again
        player.GetComponent<Rigidbody>().useGravity = true;

        //push player off of the ladder
        player.Translate(Vector3.forward * Time.deltaTime * 5);
        canClimb = true;
        player = null;
        playerObj = null;
    }

}
