using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerScriptStep : MonoBehaviour
{
    public bool isCorrect = false;
    public GameObject FloorStepManager;
    public Collider StepAnswerBubble;

    public void Start()
    {

    }

    public void OnTriggerEnter(Collider AnswerBubble) // method called when the player collides with the button
    {
          Debug.Log(" Player : Answer selected, wait 2 seconds for validation");

          StartCoroutine(waitForValidation(2)); // Waiting 3 seconds on the button to validate the button

    }


    IEnumerator waitForValidation(int time)
    {
      yield return new WaitForSeconds(time);

      Debug.Log("Answer validated");
      StepAnswersManager();
    }


    public void OnTriggerExit(Collider AnswerBubble) // method called when the player stops stepping on the answer bubble
    {
      Debug.Log("Answer deselected");
    }


    public void StepAnswersManager() // method called when the player collides with the answer bubble, and waited 3 seconds on the same answer bubble
    {
        if(isCorrect == true)
        {
            Debug.Log("Correct Answer");
            // Code for correct answer

            FloorStepManager.GetComponent<FloorStepManager>().correct();

        }


        if(isCorrect == false)
        {
            Debug.Log("Wrong Answer");
            // Code for wrong answer

            FloorStepManager.GetComponent<FloorStepManager>().wrong();
        }

    }

}
