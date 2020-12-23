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

  // Panels
  public GameObject QuestionsPanel;
  public GameObject PartialResultsPanel;
  public GameObject FinalResultsPanel;
  public GameObject UIElements;

  //From FloorQuizzManager
  public GameObject[] Floor;

  //From TimeLeft
  public GameObject TimeLeft;


  // METHODS

  private void Start()
  {
    // Set the different Panels
    QuestionsPanel.SetActive(true);
    PartialResultsPanel.SetActive(false);
    FinalResultsPanel.SetActive(false);
    UIElements.SetActive(true);

    // initialize the variables
    totalQuestions = QnA.Count;   // total number of questions
    currentQuestion = 0;

    // first method to generate all the questions
    TimeLeft.GetComponent<TimeLeft>().Start();
    generateQuestion();

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


  // Generate a question from the information provided in the Unity Inspector
  void generateQuestion()
  {
    Debug.Log("totalQuestions = " + totalQuestions);
    Debug.Log("currentQuestion = " + currentQuestion);
    if( currentQuestion < totalQuestions )
    {
      // Set the Panels
      QuestionsPanel.SetActive(true);
      // PartialResultsPanel.SetActive(false); // optional ?

      // Set the question + answer text on the wall
      QuestionTxt.text = QnA[currentQuestion].Question;
      SetAnswersWall();

      // + set the helicopter on the wall ?? => another panel ? do it with another method

      // Set the answers on the floor
      if(TimeLeft.GetComponent<TimeLeft>().GetTimerIsRunning() == true)
      {
        for(int i=0; i < Floor.Length ; i++)
        {
          Floor[i].GetComponent<FloorQuizzManager>().SetAnswersFloor(); // SET IN THE FLOOR SCRIPT !!
        }
      }
      else
      {
        Debug.Log("You can't answer anymore, Time Is Out // FloorQuizzManager");
      }

      // manage the answers given on the floor => call the method from the other script AnswerScript
      // AnswerScript.AnswersManager(); // => include time manager

      // display the intermediate results
      IntermediateResults();

    }
    else
    {
      Debug.Log("Out of questions !");

      // Game Over ??
      // Display the Final results panel ??

    }
  }

  // Display the intermediate results
  public void IntermediateResults()
  {
    // Panels
    QuestionsPanel.SetActive(false);
    PartialResultsPanel.SetActive(true);

    // Display the different scores
    for(int i=0 ; i < score.Length ; i++)
    {
      ScoreTxt[i].text = (score[i]).ToString();
    }

    // Wait for 5 seconds to display the scores
    StartCoroutine(waitForNext());

    // Panels back to question mode
    QuestionsPanel.SetActive(false);
    PartialResultsPanel.SetActive(true);

    // On to the next question (on the currentQuestion index)
    currentQuestion += 1;

    // Generate the next question
    generateQuestion();

  }

  IEnumerator waitForNext()
  {
      yield return new WaitForSeconds(10);
      generateQuestion();
  }

  //Access the QnA
  public List<QuestionAndAnswers> GetQnA()
  {
    return QnA;
  }



}
