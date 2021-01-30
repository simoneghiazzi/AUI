using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OtherMachineObstacle : MonoBehaviour
{
    //DEFINITION OF VARIABLES

    public Animator MachineAnimator;

    public GameObject TimeLeftStep;

    public GameObject WallStepManager;
    public GameObject FloorStepManager;
    public GameObject Background;

    //public ParticleSystem birds;

    bool alreadyDoneStartOtherMachine = false;


    // Start is called before the first frame update
    public void BeforeStartOtherMachine()
    {
      // INITIALIZATION OF THE SCENE

      WallStepManager.GetComponent<WallStepManager>().IntoObstacle = true;
      WallStepManager.GetComponent<WallStepManager>().TextLeoBubble.text = "How do you dodge the other machine ?";

      //Reset the Explosion
      WallStepManager.GetComponent<WallStepManager>().Explosion.SetActive(false);

      if(alreadyDoneStartOtherMachine == false)
      {
        //Leo Dialogue Balloon appears
        WallStepManager.GetComponent<WallStepManager>().LeoBubble.SetActive(true);

        //Birds displaying
        Debug.Log("Displaying OtherMachine");
        WallStepManager.GetComponent<WallStepManager>().OtherMachine.SetActive(true);
        //Birds animating
        //birds.enableEmission = false;
      }

      //Warning sign and BlackCloud displaying + animating
      WallStepManager.GetComponent<WallStepManager>().WarningSign.SetActive(true);
      WallStepManager.GetComponent<WallStepManager>().BlackWarningCloud.SetActive(true);
      WallStepManager.GetComponent<WallStepManager>().TextBlackCloud.text = "Other machine on the way !";

      //GO TO NEXT FUNCTION
      StartCoroutine(StartOtherMachine());
    }

    IEnumerator StartOtherMachine()
    {

      if(alreadyDoneStartOtherMachine == false)
      {
        Debug.Log("Starting the other machine !");

        Background.GetComponent<ScrollScript>().speed = 1f;

        for(int i = 0; i < 5 ; i++)
        {
          MachineAnimator.speed = MachineAnimator.speed - 0.1f;
          yield return new WaitForSeconds(0.5f);
        }

        //Stepwatch
        WallStepManager.GetComponent<WallStepManager>().Stepwatch.SetActive(true);


        //Done StartBirds
        alreadyDoneStartOtherMachine = true;
      }

      TimeLeftStep.GetComponent<TimeLeftStep>().StartStepTimer();

      //Display the answers on the floor
      FloorStepManager.GetComponent<FloorStepManager>().SetStepAnswersFloor();
    }

    public void StopOtherMachine()
    {
      Debug.Log("Stopping the other machine !");

      WallStepManager.GetComponent<WallStepManager>().IntoObstacle = false;

      MachineAnimator.speed = 1f;

      //Birds disappearing
      WallStepManager.GetComponent<WallStepManager>().OtherMachine.SetActive(false);

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
