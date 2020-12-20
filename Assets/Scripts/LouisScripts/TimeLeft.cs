using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeLeft : MonoBehaviour
{

    public float timeRemaining;
    public static bool timerIsRunning = false;
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
            //QuizzManager.ColorChange();
          }
          else
          {
            timeRemaining = 0;
            Debug.Log("Time has run out !");
            timerIsRunning = false;
          }
        }
    }

    public static bool GetTimerIsRunning()
    {
        return timerIsRunning;
    }


    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1; // little time adjustment to really have the correct remaining time

        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = seconds.ToString();
    }

}
