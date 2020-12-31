using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    public bool alreadyAnswered = false;

    public void Start()
    {

    }

    public void AnswersManager() // method called when the player clicks on the button
    {
        if( (isCorrect == true) && (alreadyAnswered == false))
        {
            Debug.Log("Correct Answer");
            alreadyAnswered = true;
            // Code for correct answer
            // better to put an integer to know which team is which
            // use the id of the team as the index of the score array
            // alreay answered array = INITIALIZATION false, and everytime an answer is given we put true for the corresponding team
        }

        if( alreadyAnswered == false )
        {
            Debug.Log("Time is out, you cannot answer anymore");
            // Code for Time is out
        }

        if( (isCorrect == false) && (alreadyAnswered == false))
        {
            Debug.Log("Wrong Answer");
            alreadyAnswered = true;
            // Code for wrong answer
        }

        if(alreadyAnswered == true)
        {
          Debug.Log("You already answered");
        }

    }
}
