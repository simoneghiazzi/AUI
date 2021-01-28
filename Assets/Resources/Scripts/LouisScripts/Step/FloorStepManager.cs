using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorStepManager : MonoBehaviour
{
    //VARIABLES INITIALIZATION

    public GameObject WallStepManager;

    public GameObject[] AnswerBalloons;


    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetStepAnswersFloor()
    {
      int currentObstacleFloor = WallStepManager.GetComponent<WallStepManager>().currentObstacle;

      for(int j = 0; j < AnswerBalloons.Length; j++)
      {
        // Set the answer corresponding to the index of the current question, and putting it into the
        // corresponding Text component (= corresponding text bubble)
        //AnswerBalloons[j].transform.GetChild(0).GetComponent<Text>().text = WallStepManager.GetComponent<WallStepManager>().GetOnA()[currentObstacleFloor].Answers[j];

        AnswerBalloons[j].GetComponent<AnswerScriptStep>().isCorrect = false;  // reset all the buttons to false state

        if(WallStepManager.GetComponent<WallStepManager>().GetOnA()[currentObstacleFloor].CorrectAnswer == j+1)   // if the right button is clicked (if the chosen option is in the correct index)
        {
            AnswerBalloons[j].GetComponent<AnswerScriptStep>().isCorrect = true;
        }
      }
    }

    public void correct()
    {
      //Leo happy
      WallStepManager.GetComponent<WallStepManager>().LeoHappy.SetActive(true);

      //Leo text bubble
      WallStepManager.GetComponent<WallStepManager>().TextLeoBubble.text = "Bravissimo !";

      StartCoroutine(waitForHappy());

    }

    IEnumerator waitForHappy()
    {
      yield return new WaitForSeconds(3);

      WallStepManager.GetComponent<WallStepManager>().Fly();
    }

    public void wrong()
    {
      //Leo sad
      WallStepManager.GetComponent<WallStepManager>().LeoSad.SetActive(true);

      //Leo text bubble
      WallStepManager.GetComponent<WallStepManager>().TextLeoBubble.text = "Try again !";

      StartCoroutine(waitForCrash());
    }

    IEnumerator waitForCrash()
    {

      //Explosion animation
      WallStepManager.GetComponent<WallStepManager>().Explosion.SetActive(true);

      yield return new WaitForSeconds(3);

      //Go back to the top function to reset the obstacle
      WallStepManager.GetComponent<WallStepManager>().Obstacle();
    }
}
