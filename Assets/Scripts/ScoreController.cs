using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    private int pins;
    private Text text;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    public void AddScore()
    {
        pins++;
        text.text = "Score: " + pins * 100;
    }
}
