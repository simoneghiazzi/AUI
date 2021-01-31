using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TornadoObstacle : MonoBehaviour
{
    //DEFINITION OF VARIABLES

    public Animator MachineAnimator;

    public GameObject TimeLeftStep;

    public GameObject WallStepManager;
    public GameObject FloorStepManager;
    public GameObject Background;

    //public ParticleSystem birds;

    bool alreadyDoneStartTornado = false;

    private int currentObstacle = 0;


    // Start is called before the first frame update
    public void BeforeStartTornado()
    {
      // INITIALIZATION OF THE SCENE

      currentObstacle = WallStepManager.GetComponent<WallStepManager>().currentObstacle;

      WallStepManager.GetComponent<WallStepManager>().IntoObstacle = true;
      WallStepManager.GetComponent<WallStepManager>().TextLeoBubble.text = WallStepManager.GetComponent<WallStepManager>().GetOnA()[currentObstacle].ObstacleQuestion;;

      //Reset the Explosion
      WallStepManager.GetComponent<WallStepManager>().Explosion.SetActive(false);

      if(alreadyDoneStartTornado == false)
      {
        //Leo Dialogue Balloon appears
        WallStepManager.GetComponent<WallStepManager>().LeoBubble.SetActive(true);

        //Birds displaying
        Debug.Log("Displaying Tornado");
        WallStepManager.GetComponent<WallStepManager>().Tornado.SetActive(true);
        //Birds animating
        //birds.enableEmission = false;
      }

      //Warning sign and BlackCloud displaying + animating
      WallStepManager.GetComponent<WallStepManager>().WarningSign.SetActive(true);
      WallStepManager.GetComponent<WallStepManager>().BlackWarningCloud.SetActive(true);
      WallStepManager.GetComponent<WallStepManager>().TextBlackCloud.text = "Tornado in arrivo !";

      //GO TO NEXT FUNCTION
      StartCoroutine(StartTornado());
    }

    IEnumerator StartTornado()
    {

      if(alreadyDoneStartTornado == false)
      {
        Debug.Log("Starting the tornado !");

        Background.GetComponent<ScrollScript>().speed = 1f;

        for(int i = 0; i < 5 ; i++)
        {
          MachineAnimator.speed = MachineAnimator.speed - 0.1f;
          yield return new WaitForSeconds(0.5f);
        }

        //Stepwatch
        WallStepManager.GetComponent<WallStepManager>().Stepwatch.SetActive(true);


        //Done StartBirds
        alreadyDoneStartTornado = true;
      }

      TimeLeftStep.GetComponent<TimeLeftStep>().StartStepTimer();

      //Display the answers on the floor
      FloorStepManager.GetComponent<FloorStepManager>().SetStepAnswersFloor();
    }

    public void StopTornado()
    {
      Debug.Log("Stopping the tornado !");

      MachineAnimator.speed = 1f;

      WallStepManager.GetComponent<WallStepManager>().IntoObstacle = false;

      //Tornado disappearing
      WallStepManager.GetComponent<WallStepManager>().Tornado.SetActive(false);

      //Stepwatch
      WallStepManager.GetComponent<WallStepManager>().Stepwatch.SetActive(false);

      //Leo Dialogue Balloon disappears
      WallStepManager.GetComponent<WallStepManager>().LeoBubble.SetActive(false);

      //Warning sign and BlackCloud disappearing
      WallStepManager.GetComponent<WallStepManager>().WarningSign.SetActive(false);
      WallStepManager.GetComponent<WallStepManager>().BlackWarningCloud.SetActive(false);

      //Leo waiting
      WallStepManager.GetComponent<WallStepManager>().LeoHappy.SetActive(false);
      WallStepManager.GetComponent<WallStepManager>().LeoWaiting.SetActive(true);

      WallStepManager.GetComponent<WallStepManager>().Fly();
    }

}
