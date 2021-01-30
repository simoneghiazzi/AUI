using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ThunderCloudObstacle : MonoBehaviour
{
    //DEFINITION OF VARIABLES

    public Animator MachineAnimator;

    public GameObject TimeLeftStep;

    public GameObject WallStepManager;
    public GameObject FloorStepManager;
    public GameObject Background;

    //public ParticleSystem birds;

    bool alreadyDoneStartThunderCloud = false;


    // Start is called before the first frame update
    public void BeforeStartThunderCloud()
    {
      // INITIALIZATION OF THE SCENE

      WallStepManager.GetComponent<WallStepManager>().IntoObstacle = true;
      WallStepManager.GetComponent<WallStepManager>().TextLeoBubble.text = "How do you dodge the thunder cloud ?";

      //Reset the Explosion
      WallStepManager.GetComponent<WallStepManager>().Explosion.SetActive(false);

      if(alreadyDoneStartThunderCloud == false)
      {
        //Leo Dialogue Balloon appears
        WallStepManager.GetComponent<WallStepManager>().LeoBubble.SetActive(true);

        //Birds displaying
        Debug.Log("Displaying ThunderCloud");
        WallStepManager.GetComponent<WallStepManager>().ThunderCloud.SetActive(true);
        //Birds animating
        //birds.enableEmission = false;
      }

      //Warning sign and BlackCloud displaying + animating
      WallStepManager.GetComponent<WallStepManager>().WarningSign.SetActive(true);
      WallStepManager.GetComponent<WallStepManager>().BlackWarningCloud.SetActive(true);
      WallStepManager.GetComponent<WallStepManager>().TextBlackCloud.text = "Thunder cloud on the way !";

      //GO TO NEXT FUNCTION
      StartCoroutine(StartThunderCloud());
    }

    IEnumerator StartThunderCloud()
    {

      if(alreadyDoneStartThunderCloud == false)
      {
        Debug.Log("Starting the thunder cloud !");

        Background.GetComponent<ScrollScript>().speed = 1f;

        for(int i = 0; i < 5 ; i++)
        {
          MachineAnimator.speed = MachineAnimator.speed - 0.1f;
          yield return new WaitForSeconds(0.5f);
        }

        //Stepwatch
        WallStepManager.GetComponent<WallStepManager>().Stepwatch.SetActive(true);


        //Done StartBirds
        alreadyDoneStartThunderCloud = true;
      }

      TimeLeftStep.GetComponent<TimeLeftStep>().StartStepTimer();

      //Display the answers on the floor
      FloorStepManager.GetComponent<FloorStepManager>().SetStepAnswersFloor();
    }

    public void StopThunderCloud()
    {
      Debug.Log("Stopping the thunder cloud !");

      WallStepManager.GetComponent<WallStepManager>().IntoObstacle = false;

      MachineAnimator.speed = 1f;

      //Birds disappearing
      WallStepManager.GetComponent<WallStepManager>().ThunderCloud.SetActive(false);

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
