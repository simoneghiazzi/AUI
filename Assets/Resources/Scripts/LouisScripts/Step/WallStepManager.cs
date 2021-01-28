using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WallStepManager : MonoBehaviour
{

  // VARIABLES & DECLARATION

  public List<ObstaclesAndAnswers> OnA;
  public List<Introduction> Intro;
  private int sizeofIntro;
  private int sizeofOnA;

  //Leo character
  public GameObject LeoExplaining;
  public GameObject LeoHappy;
  public GameObject LeoSad;
  public GameObject LeoWaiting;

  //Leo Text Bubble
  public GameObject LeoBubble;
  public Text TextLeoBubble;

  //BlackWarningCloud (cloud + warning + text) + warnings
  public GameObject BlackWarningCloud;
  public Text TextBlackCloud;
  public GameObject WarningSign;

  //Stepwatch
  public GameObject Stepwatch;

  //Machine
  public GameObject Machine;
  public Animator MachineAnim;

  //Explosion
  public GameObject Explosion;

  //Obstacles
  public GameObject Birds;
  public GameObject Tornado;
  public GameObject ThunderCloud;
  public GameObject OtherMachine;


  //Background
  public GameObject Background;

  //Loop Fly
  public int currentObstacle = 0;



  private void Start()
  {
    // SET UP OF THE ACTIVITY
    Debug.Log("Start WallStepManager");

    //Background static at the beginning
    Background.GetComponent<ScrollScript>().speed = 0f;

    // Leo character Explaining
    LeoExplaining.SetActive(true);
    LeoHappy.SetActive(false);
    LeoSad.SetActive(false);
    LeoWaiting.SetActive(false);

    // Dialogue Balloon
    LeoBubble.SetActive(true);

    // Black cloud + warnings
    BlackWarningCloud.SetActive(false);
    WarningSign.SetActive(false);

    // Stepwatch
    Stepwatch.SetActive(false);

    // Machine
    Machine.SetActive(false);
    MachineAnim = GetComponent<Animator>();

    //Explosion
    Explosion.SetActive(false);

    // Obstacles
    Birds.SetActive(false);
    Tornado.SetActive(false);
    ThunderCloud.SetActive(false);
    OtherMachine.SetActive(false);

    // Length of the Introduction
    sizeofIntro = Intro.Count;

    //Length of OnA
    sizeofOnA = OnA.Count;

    //Loop Fly increment
    currentObstacle = 0;


    // Introduction of the activity
    StartCoroutine(Introduction());
  }

  IEnumerator Introduction()
  {
    Debug.Log("Start Intro");
    // Leo will explain each step of the activity, it is possible to add as many introudction steps
    //  as we want in the GameManager
    for(int i = 0 ; i < sizeofIntro ; i++)
    {
      TextLeoBubble.text = Intro[i].IntroductionText;
      yield return new WaitForSeconds(3);
    }

    Debug.Log("Finish Intro");

    Fly();
  }


  public void Fly()
  {
    Debug.Log("Start Fly");
    //Background moving
    Background.GetComponent<ScrollScript>().speed = 5f;

    //Machine appearing + set the speed
    Machine.SetActive(true);

    //Leo Dialogue balloon disappearing
    LeoBubble.SetActive(false);

    if(currentObstacle < sizeofOnA)
    {
      StartCoroutine(waitForNextObstacle(10));
    }
    else
    {
      Outro();
    }

  }

  IEnumerator waitForNextObstacle(int time)
  {
    yield return new WaitForSeconds(time);

    Obstacle();
  }

  public void Obstacle()
  {

    switch(OnA[currentObstacle].Obstacle)
    {
      case "Birds":
        Debug.Log("Birds obstacle");

        Birds.GetComponent<BirdObstacle>().Start();

        break;
      case "Tornado":
        Debug.Log("Tornado obstacle");
        break;
      case "OtherMachine":
        Debug.Log("OtherMachine obstacle");
        break;
      case "ThunderCloud":
        Debug.Log("ThunderCloud obstacle");
        break;
      default:
        break;
    }
    currentObstacle++;
    Fly();
  }


  private void Outro()
  {
    //Outro of the activity
    Debug.Log("Outro");
  }


  //Access the OnA
  public List<ObstaclesAndAnswers> GetOnA()
  {
    return OnA;
  }

}
