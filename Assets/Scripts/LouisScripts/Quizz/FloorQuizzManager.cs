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
    public GameObject TimeLeft;
    public int TeamID;


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

        for(int j = 0; j < optionsTeam.Length; j++)
        {
          // Set the answer corresponding to the index of the current question, and putting it into the
          // corresponding Text component (= corresponding text bubble)
          optionsTeam[j].transform.GetChild(0).GetComponent<Text>().text = WallQuizzManager.GetComponent<WallQuizzManager>().GetQnA()[currentQuestionFloor].Answers[j];

          optionsTeam[j].GetComponent<AnswerScript>().isCorrect = false;  // reset all the buttons to false state

          if(WallQuizzManager.GetComponent<WallQuizzManager>().GetQnA()[currentQuestionFloor].CorrectAnswer == j+1)   // if the right button is clicked (if the chosen option is in the correct index)
          {
              optionsTeam[j].GetComponent<AnswerScript>().isCorrect = true;
          }
        }

    }

    public void correct()
    {
      Debug.Log("Team " + (TeamID+1) + " answered correct");

      if(WallQuizzManager.GetComponent<WallQuizzManager>().alreadyAnswered[TeamID] == false)
      {
        WallQuizzManager.GetComponent<WallQuizzManager>().score[TeamID] += 1;
        Debug.Log("+1 point for Team " + (TeamID+1));
        WallQuizzManager.GetComponent<WallQuizzManager>().alreadyAnswered[TeamID] = true;
      }
      else{
        Debug.Log("Team " + (TeamID+1) + " you already answered !");
      }
    }

    public void wrong()
    {
      Debug.Log("Team " + (TeamID+1) + " answered wrong");
      if(WallQuizzManager.GetComponent<WallQuizzManager>().alreadyAnswered[TeamID] == false)
      {
        Debug.Log("0 point for Team " + (TeamID+1));
        WallQuizzManager.GetComponent<WallQuizzManager>().alreadyAnswered[TeamID] = true;
      }
      else{
        Debug.Log("Team " + (TeamID+1) + " you already answered !");
      }
    }

}
