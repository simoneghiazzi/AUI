using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizzManager : MonoBehaviour
{
    // VARIABLES DECLARATION AND INITIALIZATION

    public List<QuestionAndAnswers> QnA;
    public GameObject[] options;
    public int currentQuestion = 0;

    public Text QuestionTxt;
    public Text ScoreTxt;

    int totalQuestions = 0; //total number of questions
    public int score = 0;


    // METHODS

    //Beginning of the Quizz game = set the question panel + UI elements
    //                              (= background, Leo character, ...)
    private void Start()
    {
        totalQuestions = QnA.Count;
        generateQuestion();
    }

    //End of the game = display the final results
    void GameOver()
    {
        ScoreTxt.text = (score).ToString();
    }

    //ANSWER
    //When an answer is correct
    public void correct()
    {
        score += 1;
        QnA.RemoveAt(currentQuestion);
    }

    //When an answer is wrong
    public void wrong()
    {
        QnA.RemoveAt(currentQuestion);
        IntermediateResults();
    }


    //Method used to manage the answers from the Unity Inspector
    void SetAnswers()
    {
        for(int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;  // reset all the buttons to false state
            options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Answers[i]; // get the text component of the button

            if(QnA[currentQuestion].CorrectAnswer == i+1)   // if the right button is clicked (if the chosen option is in the correct index)
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
          QuestionTxt.text = QnA[currentQuestion].Question; // set the current question text
          SetAnswers();
          currentQuestion += 1; // to generate the next question
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
        ScoreTxt.text = (score).ToString();
    }

}
