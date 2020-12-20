using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizzManager QuizzManager;

    public void Answer() // method called when the player clicks on the button
    {
        if( (isCorrect == true) && (TimeLeft.GetTimerIsRunning() == true) )
        {
            Debug.Log("Correct Answer");
            QuizzManager.correct();
        }
        if(TimeLeft.GetTimerIsRunning() == false)
        {
          Debug.Log("Time is out, you cannot answer anymore");
          QuizzManager.TimeIsOut();
        }

        if( (isCorrect == false) && (TimeLeft.GetTimerIsRunning() == true) )
        {
            Debug.Log("Wrong Answer");
            QuizzManager.wrong();
        }

    }
}
