using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizzManagerTeam : MonoBehaviour
{
    // VARIABLES DECLARATION AND INITIALIZATION

    public Text ScoreTxt; // score of each team in text form
    public int score = 0; // score of each team in number form

    //Get the variables from the QuizzManagerWall

    public List<QuestionAndAnswers> QnATeam;
    public int currentQuestionTeam;
    public GameObject[] optionsTeam;

    void Start(){

    }

    //ANSWER
    //When an answer is correct
    public void correct()
    {
        QnATeam.RemoveAt(currentQuestionTeam);
        score += 1;
        ScoreTxt.text = (score).ToString();
    }

    //When an answer is wrong
    public void wrong()
    {
        QnATeam.RemoveAt(currentQuestionTeam);
    }

    public void TimeIsOut()
    {
        QnATeam.RemoveAt(currentQuestionTeam);
    }


    //Method used to manage the answers from the Unity Inspector
    public void SetAnswersTeam()
    {
        for(int i = 0; i < optionsTeam.Length; i++)
        {
            optionsTeam[i].GetComponent<AnswerScript>().isCorrect = false;  // reset all the buttons to false state
            optionsTeam[i].transform.GetChild(0).GetComponent<Text>().text = QnATeam[currentQuestionTeam].Answers[i]; // get the text component of the button

            if( (QnATeam[currentQuestionTeam].CorrectAnswer == i+1) && (TimeLeft.GetTimerIsRunning() == true) )   // if the right button is clicked (if the chosen option is in the correct index)
            {
                optionsTeam[i].GetComponent<AnswerScript>().isCorrect = true;
            }

        }

    }

}
