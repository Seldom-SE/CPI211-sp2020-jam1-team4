using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform firePoint;
    public GameObject ballPrefab;
    public PlayerController mainPlayer;

    public float shootForce;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && mainPlayer.isInControl)
            ShootBall();
    }

    private void ShootBall()
    {
        GameObject ball = Instantiate(ballPrefab, firePoint.position, firePoint.rotation);
        ball.GetComponentInChildren<BallController>().BallRigidbody.AddForce(firePoint.forward * shootForce);
        ball.GetComponentInChildren<BallController>().mainPlayer = mainPlayer;
    }
}
