/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizzManager : MonoBehaviour
{
    // VARIABLES DECLARATION AND INITIALIZATION

    public List<QuestionAndAnswers> QnA;
    public GameObject[] optionsTeam1;
    public int currentQuestion = 0;

    public GameObject QuestionsPanel;
    public GameObject PartialResultsPanel;
    public GameObject FinalResultsPanel;
    public GameObject UIElements;

    public Text QuestionTxt;
    public Text ScoreTxt;

    int totalQuestions = 0; //total number of questions
    public int scoreTeam1 = 0;


    // METHODS

    //Beginning of the Quizz game = set the question panel + UI elements
    //                              (= background, Leo character, ...)
    private void Start()
    {
        totalQuestions = QnA.Count;
        PartialResultsPanel.SetActive(false);
        FinalResultsPanel.SetActive(false);
        generateQuestion();
    }

    //End of the game = display the final results
    void GameOver()
    {
        QuestionsPanel.SetActive(false);
        PartialResultsPanel.SetActive(false);
        FinalResultsPanel.SetActive(true);
<<<<<<< HEAD
        ScoreTxt.text = (score).ToString();
=======
        ScoreTxt.text = (scoreTeam1).ToString();
>>>>>>> parent of 4382270... Multiplayer partially works
    }

    //ANSWER
    //When an answer is correct
    public void correct()
    {
        scoreTeam1 += 1;
        QnA.RemoveAt(currentQuestion);
        IntermediateResults();
        StartCoroutine(waitForNext());
    }

    //When an answer is wrong
    public void wrong()
    {
        QnA.RemoveAt(currentQuestion);
        IntermediateResults();
        StartCoroutine(waitForNext());
<<<<<<< HEAD
    }

    public void TimeIsOut()
    {
        QnA.RemoveAt(currentQuestion);
        IntermediateResults();
        StartCoroutine(waitForNext());
    }

    //Wait for next question
    IEnumerator waitForNext()
    {
        yield return new WaitForSeconds(5);
        generateQuestion();
=======
>>>>>>> parent of 4382270... Multiplayer partially works
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
        //TEAM1
        for(int i = 0; i < optionsTeam1.Length; i++)
        {
            optionsTeam1[i].GetComponent<AnswerScript>().isCorrect = false;  // reset all the buttons to false state
            optionsTeam1[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Answers[i]; // get the text component of the button

            if( (QnA[currentQuestion].CorrectAnswer == i+1) && (TimeLeft.GetTimerIsRunning() == true))   // if the right button is clicked (if the chosen option is in the correct index)
            {
                optionsTeam1[i].GetComponent<AnswerScript>().isCorrect = true;
            }

        }

    }

    //Method used to generate a question from the Unity Inspector
    void generateQuestion()
    {
        if(QnA.Count > 0) //when there are questions available
        {
          currentQuestion = Random.Range(0, QnA.Count); // generate a random question from the list of questions provided

          QuestionTxt.text = QnA[currentQuestion].Question; // set the current question text
          SetAnswers();
          QuestionsPanel.SetActive(true);
          PartialResultsPanel.SetActive(false);
<<<<<<< HEAD
          currentQuestion += 1; // to generate the next question
=======
>>>>>>> parent of 4382270... Multiplayer partially works
        }
        else //when there are no more questions
        {
          Debug.Log("Out of Questions");
          GameOver();
        }

    }

    //After each question = display the intermediate results
    void IntermediateResults()
    {
<<<<<<< HEAD
        QuestionsPanel.SetActive(false);
        PartialResultsPanel.SetActive(true);
        ScoreTxt.text = (score).ToString();
=======
        ScoreTxt.text = (scoreTeam1).ToString();
        QuestionsPanel.SetActive(false);
        PartialResultsPanel.SetActive(true);
>>>>>>> parent of 4382270... Multiplayer partially works
    }

    public void ColorChange()
    {
        GetComponent<Renderer>().material.color = Color.Lerp(Color.green, Color.red, Time.time);
    }

}
*/
