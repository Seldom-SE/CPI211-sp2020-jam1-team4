using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    public int duration;

    private float startTime;
    private Text text;

    void Start()
    {
        startTime = Time.time;
        text = GetComponent<Text>();
    }

    void Update()
    {
        int timeRemaining = duration - (int)(Time.time - startTime);
        if (timeRemaining <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("Game Over");
        }
        text.text = "TIME: " + timeRemaining / 60 + ':' + (timeRemaining % 60).ToString("00");
    }
}
