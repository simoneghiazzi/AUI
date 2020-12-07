using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizzManager QuizzManager;

    public void Answer()
    {
        if(isCorrect)
        {
            Debug.Log("Correct Answer");
            QuizzManager.correct();
        }

        else
        {
            Debug.Log("Wrong Answer");
            QuizzManager.correct();
        }


    }
}
