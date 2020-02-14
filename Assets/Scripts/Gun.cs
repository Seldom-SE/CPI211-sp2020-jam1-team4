using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public float bulletVelocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))  
        {
            GameObject obj = Instantiate(bullet,transform.position + transform.forward*1, Quaternion.identity);
            obj.GetComponent<Bullet>().speed = bulletVelocity;
            obj.GetComponent<Bullet>().direction = transform.GetChild(0).transform.forward;
        } 
    }
}
