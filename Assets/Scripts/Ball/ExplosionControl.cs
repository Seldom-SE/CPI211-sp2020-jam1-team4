using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class on the ball follower obj that controls the explosion
/// 
/// The way the explosion works is that this obj has a trigger
/// collider that acts as the radius for the explosion.All obj's
/// tagged with "Pin" and inside this collider will be hit with
/// the explosion
/// 
/// Note that this class MUST be on the ball follower for the trigger collider
/// to essentially follow the ball
/// </summary>
public class ExplosionControl : MonoBehaviour
{
    public GameObject parentBallObj;    //this is the highest parent obj for the ball
    public GameObject ballObj;  //Reference to the ball obj that is being followed

    [Header("Explosion")]
    public float explPower;
    private List<Rigidbody> _explodableObjects;

    private void Awake()
    {
        _explodableObjects = new List<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Explode();
    }

    /// <summary>
    /// Adds a colliding pin or obstacle into the list
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pin") || other.gameObject.CompareTag("Obstacle"))
        {
            _explodableObjects.Add(other.gameObject.GetComponent<Rigidbody>());
        }
    }

    /// <summary>
    /// Removes a pin or obstacle that used to be colliding
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if ((other.gameObject.CompareTag("Pin") || other.gameObject.CompareTag("Obstacle")) && _explodableObjects.Contains(other.gameObject.GetComponent<Rigidbody>()))
        {
            _explodableObjects.Remove(other.gameObject.GetComponent<Rigidbody>());
        }
    }

    /// <summary>
    /// Method that adds the explosive force to the colliding pins
    /// </summary>
    public void Explode()
    {
        
        foreach (Rigidbody rb in _explodableObjects)
        {
            //Note: I put a 0 for radius to ensure that all balls within the list are being hit by the force
            //Note: The 3.0 for upward modifier was a default value that I felt looked good. Change if needed/desired
            rb.AddExplosionForce(explPower, ballObj.transform.position, 0, 3.0f);
        }

        /**
         * Transition to player here
         */

        Destroy(parentBallObj);
    }
}
