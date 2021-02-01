using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FloorStepManager : MonoBehaviour
{
    //VARIABLES INITIALIZATION

    public GameObject WallStepManager;

    public GameObject[] AnswerBalloons;

    public GameObject BirdObstacle;
    public GameObject TornadoObstacle;
    public GameObject ThunderCloudObstacle;
    public GameObject OtherMachineObstacle;

    public GameObject TimeLeftStep;

    private int currentObstacleFloor = 0;


    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetStepAnswersFloor()
    {
      currentObstacleFloor = WallStepManager.GetComponent<WallStepManager>().currentObstacle;

      for(int j = 0; j < AnswerBalloons.Length; j++)
      {
        // Set the answer corresponding to the index of the current question, and putting it into the
        // corresponding Text component (= corresponding text bubble)
        AnswerBalloons[j].transform.GetChild(0).GetComponent<Text>().text = WallStepManager.GetComponent<WallStepManager>().GetOnA()[currentObstacleFloor].Answers[j];

        AnswerBalloons[j].GetComponent<AnswerScriptStep>().isCorrect = false;  // reset all the buttons to false state

        if(WallStepManager.GetComponent<WallStepManager>().GetOnA()[currentObstacleFloor].CorrectAnswer == j+1)   // if the right button is clicked (if the chosen option is in the correct index)
        {
            AnswerBalloons[j].GetComponent<AnswerScriptStep>().isCorrect = true;
        }
      }
    }

    public void correct()
    {
      //Stop timer
      TimeLeftStep.GetComponent<TimeLeftStep>().StopStepTimer();

      //Leo happy
      WallStepManager.GetComponent<WallStepManager>().LeoWaiting.SetActive(false);
      WallStepManager.GetComponent<WallStepManager>().LeoHappy.SetActive(true);

      //Leo text bubble
      WallStepManager.GetComponent<WallStepManager>().LeoBubble.SetActive(true);
      WallStepManager.GetComponent<WallStepManager>().TextLeoBubble.text = WallStepManager.GetComponent<WallStepManager>().GetOnA()[currentObstacleFloor].FeedbackText;

      //incrementing the obstacle variable to go on the next Obstacle
      WallStepManager.GetComponent<WallStepManager>().currentObstacle++;

      StartCoroutine(waitForHappy());

    }

    IEnumerator waitForHappy()
    {
      yield return new WaitForSeconds(8);

      if(WallStepManager.GetComponent<WallStepManager>().GetOnA()[currentObstacleFloor].Obstacle == "Birds")
      {
        BirdObstacle.GetComponent<BirdObstacle>().StopBirds();
      }
      if(WallStepManager.GetComponent<WallStepManager>().GetOnA()[currentObstacleFloor].Obstacle == "Tornado")
      {
        TornadoObstacle.GetComponent<TornadoObstacle>().StopTornado();
      }
      if(WallStepManager.GetComponent<WallStepManager>().GetOnA()[currentObstacleFloor].Obstacle == "OtherMachine")
      {
        OtherMachineObstacle.GetComponent<OtherMachineObstacle>().StopOtherMachine();
      }
      if(WallStepManager.GetComponent<WallStepManager>().GetOnA()[currentObstacleFloor].Obstacle == "ThunderCloud")
      {
        ThunderCloudObstacle.GetComponent<ThunderCloudObstacle>().StopThunderCloud();
      }

    }

    public void wrong()
    {
      //Leo sad
      WallStepManager.GetComponent<WallStepManager>().LeoWaiting.SetActive(false);
      WallStepManager.GetComponent<WallStepManager>().LeoSad.SetActive(true);

      //Leo text bubble
      WallStepManager.GetComponent<WallStepManager>().LeoBubble.SetActive(true);
      WallStepManager.GetComponent<WallStepManager>().TextLeoBubble.text = WallStepManager.GetComponent<WallStepManager>().GetOnA()[currentObstacleFloor].WrongAnswerFeedback;

      StartCoroutine(waitForCrash());
    }

    IEnumerator waitForCrash()
    {
      Debug.Log("Crash !!");
      //Explosion animation
      WallStepManager.GetComponent<WallStepManager>().Explosion.SetActive(true);

      yield return new WaitForSeconds(5);

      //Go back to the top function to reset the obstacle
      WallStepManager.GetComponent<WallStepManager>().Obstacle();
    }
}
