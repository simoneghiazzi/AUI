using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WallStepManager : MonoBehaviour
{

  // VARIABLES & DECLARATION

  public int nbObstacles = 0;

  public List<ObstaclesAndAnswers> OnA;

  //Leo character
  public GameObject LeoExplaining;
  public GameObject LeoHappy;
  public GameObject LeoSad;
  public GameObject LeoWaiting;

  //Leo Text Bubble
  public GameObject TextBubble;
  public GameObject DialogueBalloon;

  //BlackCloud + warnings
  public GameObject BlackCloud;
  public GameObject WarningSignCloud;
  public GameObject WarningSign;

  //Machine
  public GameObject Machine;

  //Obstacles
  public GameObject Birds;
  public GameObject Tornado;
  public GameObject ThunderCloud;
  public GameObject OtherMachine;



  private void Start()
  {
    // SET UP OF THE ACTIVITY

    //Leo Explaining
    LeoExplaining.SetActive(true);
    LeoHappy.SetActive(false);
    LeoSad.SetActive(false);
    LeoWaiting.SetActive(false);


    // Black cloud + warnings


    // Introduction of the activity
    Introduction();
  }

  private void Introduction()
  {
    // Leo presents the activity

    // Leo asks for 4 children to play at turn

    // Display the machine ready to fly (pause mode)

    // Wait for confirmation of one children to start the activity

    // If the confirmation is yes, start activity
    Fly();
  }

  private void Fly()
  {
    // wait 20 seconds beetween each obstacle

    // 4 obstacles => loop of four elements (nbSteps)

  }

}
