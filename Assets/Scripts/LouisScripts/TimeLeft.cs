using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeLeft : MonoBehaviour
{

    public float timeRemaining;
    public bool timerIsRunning = false;
    public Text timeText;

    private void Start()
    {
      //Starts the timer automatically
      timerIsRunning = true;
    }

    void Update()
    {
        if(timerIsRunning)
        {
          if (timeRemaining > 0)
          {
            timeRemaining -= Time.deltaTime;
            DisplayTime(timeRemaining);
          }
          else
          {
            Debug.Log("Time has run out !");
            timeRemaining = 0;
            timerIsRunning = false;
          }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1; // little time adjustment to really have the correct remaining time

        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = seconds.ToString();
    }

}
