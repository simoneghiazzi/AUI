using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizzManager quizzManager;

    public void Answer()
    {
        if(isCorrect)
        {
            Debug.log("Correct Answer");
            quizzManager.correct();
        }

        else
        {
            Debug.log("Wrong Answer");
            quizzManager.correct();
        }

    
    }
}
