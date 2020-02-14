using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 direction;
    public float speed = 1.0f;
    public Rigidbody physics;

    void Start()
    {
           physics.AddForce(transform.forward * speed);
    }

    void FixedUpdate()
    {

    }
}
