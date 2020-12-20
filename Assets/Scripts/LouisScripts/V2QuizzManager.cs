//V2
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
    public GameObject[] optionsTeam2;
    public GameObject[] optionsTeam3;
    public GameObject[] optionsTeam4;
    public GameObject[] optionsWall;
    public int currentQuestion = 0;

    public GameObject QuestionsPanel;
    public GameObject PartialResultsPanel;
    public GameObject FinalResultsPanel;
    public GameObject UIElements;

    public Text QuestionTxt;
    public Text ScoreTxt;

    int totalQuestions = 0; //total number of questions
    public int scoreTeam1 = 0;
    public int scoreTeam2 = 0;
    public int scoreTeam3 = 0;
    public int scoreTeam4 = 0;


    //METHODS

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
        //TEAM1
        for(int i = 0; i < optionsTeam1.Length; i++)
        {
            optionsTeam1[i].GetComponent<AnswerScript>().isCorrect = false;  // reset all the buttons to false state
            optionsTeam1[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Answers[i]; // get the text component of the button

            if(QnA[currentQuestion].CorrectAnswer == i+1)   // if the right button is clicked (if the chosen option is in the correct index)
            {
                optionsTeam1[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }

        //TEAM2
        for(int j = 0; j < optionsTeam2.Length; j++)
        {
            optionsTeam2[j].GetComponent<AnswerScript>().isCorrect = false;  // reset all the buttons to false state
            optionsTeam2[j].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Answers[j]; // get the text component of the button

            if(QnA[currentQuestion].CorrectAnswer == j+1)   // if the right button is clicked (if the chosen option is in the correct index)
            {
                optionsTeam2[j].GetComponent<AnswerScript>().isCorrect = true;
            }
        }

        //TEAM3
        for(int k = 0; k < optionsTeam3.Length; k++)
        {
            optionsTeam3[k].GetComponent<AnswerScript>().isCorrect = false;  // reset all the buttons to false state
            optionsTeam3[k].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Answers[k]; // get the text component of the button

            if(QnA[currentQuestion].CorrectAnswer == k+1)   // if the right button is clicked (if the chosen option is in the correct index)
            {
                optionsTeam3[k].GetComponent<AnswerScript>().isCorrect = true;
            }
        }

        //TEAM4
        for(int l = 0; l < optionsTeam4.Length; l++)
        {
            optionsTeam4[l].GetComponent<AnswerScript>().isCorrect = false;  // reset all the buttons to false state
            optionsTeam4[l].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Answers[l]; // get the text component of the button

            if(QnA[currentQuestion].CorrectAnswer == l+1)   // if the right button is clicked (if the chosen option is in the correct index)
            {
                optionsTeam4[l].GetComponent<AnswerScript>().isCorrect = true;
            }
        }

        //WALL
        for(int m = 0; m < optionsWall.Length; m++)
        {
            optionsWall[m].GetComponent<AnswerScript>().isCorrect = false;  // reset all the buttons to false state
            optionsWall[m].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Answers[m]; // get the text component of the button

            if(QnA[currentQuestion].CorrectAnswer == m+1)   // if the right button is clicked (if the chosen option is in the correct index)
            {
                optionsWall[m].GetComponent<AnswerScript>().isCorrect = true;
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

*/




//V1
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
        ScoreTxt.text = (scoreTeam1).ToString();
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
        //TEAM1
        for(int i = 0; i < optionsTeam1.Length; i++)
        {
            optionsTeam1[i].GetComponent<AnswerScript>().isCorrect = false;  // reset all the buttons to false state
            optionsTeam1[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Answers[i]; // get the text component of the button

            if(QnA[currentQuestion].CorrectAnswer == i+1)   // if the right button is clicked (if the chosen option is in the correct index)
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
*/
