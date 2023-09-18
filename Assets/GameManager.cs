using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : MonoBehaviour
{
    [Header("Timer")]
    public float timeLeft;
    public bool timerOn = false;
    public TMP_Text timerText;

    [Header("Debugging")]
    public KeyCode resetScene;
    

    private void Start()
    {
        timerOn = true;
    }

    private void Update()
    {
        if (timerOn)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                updateTimer(timeLeft);
            }
            else
            {
                Debug.Log("Time is Over");
                timeLeft = 0;
                timerOn = false;
            }
        }

        if (Input.GetKeyDown(resetScene))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;

        float mins = Mathf.FloorToInt(currentTime / 60);
        float secs = Mathf.FloorToInt(currentTime % 60);

        timerText.SetText("Timer:  " + mins + " : " + secs);
    }
}
