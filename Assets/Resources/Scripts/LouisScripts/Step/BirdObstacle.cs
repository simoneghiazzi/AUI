using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdObstacle : MonoBehaviour
{
    //DEFINITION OF VARIABLES

    public Animator MachineAnimator;

    public GameObject TimeLeftStep;

    public GameObject WallStepManager;
    public GameObject FloorStepManager;
    public GameObject Background;

    public ParticleSystem birds;


    // Start is called before the first frame update
    public void Start()
    {
      // INITIALIZATION OF THE SCENE

      //Background slowing down
      Background.GetComponent<ScrollScript>().speed = 0.5f;

      //Machine slowing down
      MachineAnimator.speed = 0.1f;

      //Leo Dialogue Balloon appears
      WallStepManager.GetComponent<WallStepManager>().LeoBubble.SetActive(true);


      //Birds displaying
      WallStepManager.GetComponent<WallStepManager>().Birds.SetActive(true);
      //Birds animating
      birds.enableEmission = true;

      //Warning sign displaying + animating
      WallStepManager.GetComponent<WallStepManager>().WarningSign.SetActive(true);

      //GO TO NEXT FUNCTION
      StartBirds();
    }

    void StartBirds()
    {
      Debug.Log("Starting the birds !");

      //Start the timer
      TimeLeftStep.GetComponent<TimeLeftStep>().StartStepTimer();

      //Display the answers on the floor
      FloorStepManager.GetComponent<FloorStepManager>().SetStepAnswersFloor();
    }

    void StopBirds()
    {
      Debug.Log("Stopping the birds !");

      WallStepManager.GetComponent<WallStepManager>().Fly();
    }

}
