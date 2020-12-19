using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizzManagerTeam QuizzManagerTeam;

    public void Answer() // method called when the player clicks on the button
    {
        if( (isCorrect == true) && (TimeLeft.GetTimerIsRunning() == true) )
        {
            Debug.Log("Correct Answer");
            QuizzManagerTeam.correct();
        }
        if(TimeLeft.GetTimerIsRunning() == false)
        {
          Debug.Log("Time is out, you cannot answer anymore");
          QuizzManagerTeam.TimeIsOut();
        }

        if( (isCorrect == false) && (TimeLeft.GetTimerIsRunning() == true) )
        {
            Debug.Log("Wrong Answer");
            QuizzManagerTeam.wrong();
        }

    }
}
