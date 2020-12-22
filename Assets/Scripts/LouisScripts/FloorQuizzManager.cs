using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloorQuizzManager : MonoBehaviour
{
    // VARIABLES INITIALIZATION
    public GameObject WallQuizzManager;
    public GameObject[] optionsTeam;
    WallQuizzManager Wall = new WallQuizzManager();


    // Start is called before the first frame update
    void Start()
    {
        // Timer starts automatically in the TimeLeft script

        //Initialize optionsTeam variable
        /*
        for(int i = 0; i < Wall.options.Length ; i++)
        {
          optionsTeam[i] = Wall.options[i]; // make a copy
        }
        */


    }


    // Set the answers in the different bubbles on the floor
    public void SetAnswersFloor()
    {
      int currentQuestionFloor = WallQuizzManager.GetComponent<WallQuizzManager>().currentQuestion;

        for(int i = 0; i < optionsTeam.Length; i++)
        {
          // Set the answer corresponding to the index of the current question, and putting it into the
          // corresponding Text component (= corresponding text bubble)
          optionsTeam[i].transform.GetChild(0).GetComponent<Text>().text = Wall.GetQnA()[currentQuestionFloor].Answers[i];

          optionsTeam[i].GetComponent<AnswerScript>().isCorrect = false;  // reset all the buttons to false state

          if(Wall.GetQnA()[currentQuestionFloor].CorrectAnswer == i+1)   // if the right button is clicked (if the chosen option is in the correct index)
          {
              optionsTeam[i].GetComponent<AnswerScript>().isCorrect = true;
          }
        }
    }


}
