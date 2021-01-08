using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    public GameObject FloorQuizzManager;

    public void Start()
    {

    }

    public void AnswersManager() // method called when the player clicks on the button
    {
        if(isCorrect == true)
        {
            Debug.Log("Correct Answer");
            // Code for correct answer

            FloorQuizzManager.GetComponent<FloorQuizzManager>().correct();

            // better to put an integer to know which team is which
            // use the id of the team as the index of the score array
            // alreay answered array = INITIALIZATION false, and everytime an answer is given we put true for the corresponding team
        }
/*
        if( alreadyAnswered == false )
        {
            Debug.Log("Time is out, you cannot answer anymore");
            // Code for Time is out
        }
*/
        if(isCorrect == false)
        {
            Debug.Log("Wrong Answer");
            // Code for wrong answer

            FloorQuizzManager.GetComponent<FloorQuizzManager>().wrong();
        }

    }
}
