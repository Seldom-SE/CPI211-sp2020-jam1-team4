using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stopClimb : MonoBehaviour
{
    //init vars
    Transform player;
    public float climbSpeed;

    //prevent player from descending below the ladder
    void OnTriggerStay(Collider other)
    {
        player = other.transform;

        if(Input.GetAxisRaw("Vertical") < 0)
        {
            player.transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * climbSpeed);
        }
    }

    //prevent player from descending below the ladder
    void OnTriggerEnter(Collider other)
    {
        player = other.transform;

        if (Input.GetAxisRaw("Vertical") < 0)
        {
            player.transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * climbSpeed);
        }
    }

    //push player off of the ladder
    void OnTriggerExit(Collider other)
    {
        player = other.transform;

        //push player off of the ladder
        player.Translate(-Vector3.forward * Time.deltaTime * 20);
        player = null;
    }

}
