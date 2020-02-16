using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pin : MonoBehaviour
{
    private bool fallen;
    private ScoreController scoreObject;

    private void Start()
    {
        scoreObject = GameObject.Find("Score").GetComponent<ScoreController>();
    }

    private void Update()
    {
        if (!fallen && gameObject.GetComponent<Rigidbody>().velocity.magnitude > 1)
        {
            scoreObject.AddScore();
            fallen = true;
        }
    }
}
