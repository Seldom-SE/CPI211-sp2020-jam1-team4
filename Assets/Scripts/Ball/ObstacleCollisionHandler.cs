using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple class on the actual ball obj that causes an
/// explosion when colliding with an obstacle
/// 
/// Note: For now the ball does not explode when in contact with the pins
/// </summary>
public class ObstacleCollisionHandler : MonoBehaviour
{
    public ExplosionControl explosionControl;

    public float pushBackModifier;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Obstacle"))
        {
            //explosionControl.Explode();
            GetComponent<Rigidbody>().AddForce((transform.position - collision.transform.position) * pushBackModifier);
        }
    }
}
