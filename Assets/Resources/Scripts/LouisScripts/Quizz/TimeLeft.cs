using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeLeft : MonoBehaviour
{

    public float timeRemaining;
    public bool timerIsRunning = false;
    public Text timeText;

    public GameObject StepwatchBubble;

    public GameObject WallQuizzManager;

    public GameObject PlayerController;

    public void StartTimer()
    {
      Debug.Log("Start Timer");
      //Starts the timer automatically
      timerIsRunning = true;
      timeRemaining = 15;

    }

    void Update()
    {
        if(timerIsRunning == true)
        {
          if (timeRemaining > 0)
          {
            timeRemaining -= Time.deltaTime;
            DisplayTime(timeRemaining);
            //Debug.Log("Time remaining = " + DisplayTimeDebug(timeRemaining));

            // Turn the color of the stepwatch bubble to orange
            if (timeRemaining < 5 )
            {
              StepwatchBubble.GetComponent<ChangeColor>().AlmostFinishedColor();
            }
            else
            {
              //Reset the color of the stepwatch bubble to green
              StepwatchBubble.GetComponent<ChangeColor>().ResetColor();
            }
          }
          else
          {
            timeRemaining = 0;
            Debug.Log("Time has run out ! You can't answer anymore");

            //To prevent teams from answering after the time has run out
            for(int j=0 ; j < WallQuizzManager.GetComponent<WallQuizzManager>().alreadyAnswered.Length ; j++)
            {
              WallQuizzManager.GetComponent<WallQuizzManager>().alreadyAnswered[j] = true;
            }

            timerIsRunning = false;

            // Turn the color of the stepwatch bubble to red
            StepwatchBubble.GetComponent<ChangeColor>().FinalColor();

            WallQuizzManager.GetComponent<WallQuizzManager>().TimeIsOut();
          }
        }
    }

    public bool GetTimerIsRunning()
    {
        return timerIsRunning;
    }


    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1; // little time adjustment to really have the correct remaining time

        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = seconds.ToString();
    }

    string DisplayTimeDebug(float timeToDisplay)
    {
      timeToDisplay += 1; // little time adjustment to really have the correct remaining time

      float seconds = Mathf.FloorToInt(timeToDisplay % 60);

      return timeText.text = seconds.ToString();
    }

}
