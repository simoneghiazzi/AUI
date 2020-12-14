using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizzManager : MonoBehaviour
{
    /* VARIABLES DECLARATION AND INITIALIZATION*/

    public List<QuestionAndAnswers> QnA;
    public GameObject[] options;
    public int currentQuestion = 0;

    public GameObject QuestionsPanel;
    public GameObject PartialResultsPanel;
    public GameObject FinalResultsPanel;
    public GameObject UIElements;

    public Text QuestionTxt;
    public Text ScoreTxt;

    int totalQuestions = 0; //total number of questions
    public int scoreTeam1 = 0;


    /* METHODS */

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
        ScoreTxt.text = (scoreTeam1).ToString();
    }

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
    }

    //Wait for next question
    IEnumerator waitForNext()
    {
        yield return new WaitForSeconds(5);
        generateQuestion();
    }

    //Method used to manage the answers from the Unity Inspector
    void SetAnswers()
    {
        for(int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Answers[i];

            if(QnA[currentQuestion].CorrectAnswer == i+1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }

    //Method used to generate a question from the Unity Inspector
    void generateQuestion()
    {
        if(QnA.Count > 0) //when there are questions available
        {
          currentQuestion = Random.Range(0, QnA.Count);

          QuestionTxt.text = QnA[currentQuestion].Question;
          SetAnswers();
          QuestionsPanel.SetActive(true);
          PartialResultsPanel.SetActive(false);
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
        ScoreTxt.text = (scoreTeam1).ToString();
        QuestionsPanel.SetActive(false);
        PartialResultsPanel.SetActive(true);
    }

}
