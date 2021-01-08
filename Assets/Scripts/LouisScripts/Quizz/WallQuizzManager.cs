using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WallQuizzManager : MonoBehaviour
{
  // VARIABLES DECLARATION AND INITIALIZATION

  public List<QuestionAndAnswers> QnA;
  public GameObject[] options;

  public int currentQuestion = 0;   // index of the current question
  int totalQuestions = 0;   // total number of questions
  public Text QuestionTxt;    // text of the current question
  public int[] score = new int[4]; // array contening the score of each team
  public Text[] ScoreTxt = new Text[4];   // array of score in text of each team
  public bool[] alreadyAnswered = new bool [4];

  // Panels
  public GameObject QuestionsPanel;
  public GameObject PartialResultsPanel;
  public GameObject FinalResultsPanel;
  public GameObject UIElements;

  //From FloorQuizzManager
  public GameObject[] Floor; // to be linked to all the FloorQuizzManager scripts used

  //From TimeLeft
  public GameObject TimeLeft;


  // METHODS

  private void Start()
  {
    Debug.Log("Start WallQuizzManager");

    // Set the different Panels
    QuestionsPanel.SetActive(true);
    PartialResultsPanel.SetActive(false);
    FinalResultsPanel.SetActive(false);
    UIElements.SetActive(true);

    // initialize the variables
    totalQuestions = QnA.Count;   // total number of questions
    currentQuestion = 0;

    Debug.Log("Start Finished");

    // first method to generate all the questions
    generateQuestion();

  }



  // Generate a question from the information provided in the Unity Inspector
  void generateQuestion()
  {
    Debug.Log("generateQuestion");
    Debug.Log("currentQuestion = " + currentQuestion);

    //Re-initialization of the alreadyAnswered array
    for(int k = 0 ; k < alreadyAnswered.Length ; k++){
      alreadyAnswered[k] = false;
      //Debug.Log("State " + k + " = " + alreadyAnswered[k]);
    }

    TimeLeft.GetComponent<TimeLeft>().StartTimer();

      // Set the question + answer text on the wall
      QuestionTxt.text = QnA[currentQuestion].Question;
      SetAnswersWall();

      // + set the helicopter on the wall ?? => another panel ? do it with another method

      // Set the answers on the floor
      for(int i=0; i < Floor.Length ; i++)
        {
          //Debug.Log("hey SetAnswersFloor" + i);
          Floor[i].GetComponent<FloorQuizzManager>().SetAnswersFloor(); // Set the answers of each FloorQuizzManager
        }

  }



  // Manage the answers displayed on the Wall
  public void SetAnswersWall()
  {
    for(int i = 0; i < options.Length; i++)
    {
      // Set the answer corresponding to the index of the current question, and putting it into the
      // corresponding Text component (= corresponding text bubble)
      options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Answers[i];
    }
  }



  // Display the intermediate results
  public void IntermediateResults()
  {
    Debug.Log("IntermediateResults");

    // Panels
    QuestionsPanel.SetActive(false);
    PartialResultsPanel.SetActive(true);

    // Display the different scores
    for(int i=0 ; i < score.Length ; i++)
    {
      ScoreTxt[i].text = (score[i]).ToString();
    }

    // Wait for 5 seconds to display the scores
    StartCoroutine(waitForNext(10));

    // On to the next question (on the currentQuestion index)
    currentQuestion += 1;

  }



  // Display the final results
  public void FinalResults()
  {
    Debug.Log("FinalResults");

    // Panels
    QuestionsPanel.SetActive(false);
    PartialResultsPanel.SetActive(true);

    // Display the different scores
    for(int i=0 ; i < score.Length ; i++)
    {
      ScoreTxt[i].text = (score[i]).ToString();
    }

    // Wait for 5 seconds to display the scores
    StartCoroutine(waitForNext(10));

    Debug.Log("END OF GAME");

  }



  IEnumerator waitForNext(int time)
  {
      yield return new WaitForSeconds(time);

      // Panels back to question mode
      QuestionsPanel.SetActive(true);
      PartialResultsPanel.SetActive(false);

      //Check if there are no more questions
      if(currentQuestion > totalQuestions)
      {
        FinalResults();
      }
      else
      {
        // Generate the next question
        generateQuestion();
      }
  }



  //Access the QnA
  public List<QuestionAndAnswers> GetQnA()
  {
    return QnA;
  }



}
