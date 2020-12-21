using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    public GameObject myObject1;

    public void Start()
    {
      myObject1 = GameObject.Find("QuizzManagerTeam1");
    }

    public void Answer() // method called when the player clicks on the button
    {
        if( (isCorrect == true) && (TimeLeft.GetTimerIsRunning() == true) )
        {
            Debug.Log("Correct Answer");
            myObject1.GetComponent<QuizzManager>().correct();
        }

        if(TimeLeft.GetTimerIsRunning() == false)
        {
            Debug.Log("Time is out, you cannot answer anymore");
            myObject1.GetComponent<QuizzManager>().TimeIsOut();
        }

        if( (isCorrect == false) && (TimeLeft.GetTimerIsRunning() == true) )
        {
            Debug.Log("Wrong Answer");
            myObject1.GetComponent<QuizzManager>().wrong();
        }

    }
}
