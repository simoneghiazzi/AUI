using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    public GameObject FloorQuizzManager;
    public Collider AnswerBubble;

    public void Start()
    {

    }

    public void OnTriggerEnter(Collider AnswerBubble) // method called when the player collides with the button
    {
        Debug.Log("Answer selected, wait 2 seconds for validation");

        StartCoroutine(waitForValidation(2)); // Waiting 3 seconds on the button to validate the button
    }


    IEnumerator waitForValidation(int time)
    {
      yield return new WaitForSeconds(time);

      Debug.Log("Answer validated");
      AnswersManager();
    }


    public void OnTriggerExit(Collider AnswerBubble) // method called when the player stops stepping on the answer bubble
    {
      Debug.Log("Answer deselected");
    }


    public void AnswersManager() // method called when the player collides with the answer bubble, and waited 3 seconds on the same answer bubble
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
