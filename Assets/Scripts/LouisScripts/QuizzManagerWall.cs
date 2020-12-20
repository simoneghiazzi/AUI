using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizzManagerWall : MonoBehaviour
{
  // VARIABLES DECLARATION AND INITIALIZATION

  public List<QuestionAndAnswers> QnA;
  public GameObject[] options;
  public int currentQuestion = 0;

  public GameObject QuestionsPanel;
  public GameObject PartialResultsPanel;
  public GameObject FinalResultsPanel;
  public GameObject UIElements;

  public Text QuestionTxt;

  int totalQuestions = 0; //total number of questions

  QuizzManagerTeam QMTeam;


  // METHODS

  //Beginning of the Quizz game = set the question panel + UI elements
  //                              (= background, Leo character, ...)
  public void Start()
  {
      totalQuestions = QnA.Count;
      PartialResultsPanel.SetActive(false);
      FinalResultsPanel.SetActive(false);
      generateQuestion();
  }

  //End of the game = display the final results
  public void GameOver()
  {
      QuestionsPanel.SetActive(false);
      PartialResultsPanel.SetActive(false);
      FinalResultsPanel.SetActive(true);
      StartCoroutine(waitForNext());
  }


  //Wait for next question
  IEnumerator waitForNext()
  {
      yield return new WaitForSeconds(5);
      generateQuestion();
  }


  //Method used to manage the answers from the Unity Inspector
  public void SetAnswers()
  {
      for(int i = 0; i < options.Length; i++)
      {
          options[i].GetComponent<AnswerScript>().isCorrect = false;  // reset all the buttons to false state
          options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Answers[i]; // get the text component of the button
      }

  }

  //Method used to generate a question from the Unity Inspector
  public void generateQuestion()
  {
      if(QnA.Count > 0) //when there are questions available
      {
        QuestionTxt.text = QnA[currentQuestion].Question; // set the current question text
        SetAnswers();
        QMTeam.SetAnswersTeam();
        QuestionsPanel.SetActive(true);
        PartialResultsPanel.SetActive(false);
        currentQuestion += 1; // to generate the next question
      }
      else //when there are no more questions
      {
        Debug.Log("Out of Questions");
        GameOver();
      }

  }

  //After each question = display the intermediate results
  public void IntermediateResults()
  {
      QuestionsPanel.SetActive(false);
      PartialResultsPanel.SetActive(true);
  }

  public void ColorChange()
  {
      GetComponent<Renderer>().material.color = Color.Lerp(Color.green, Color.red, Time.time);
  }

  public int GetCurrentQuestion(){
    return currentQuestion;
  }

}
