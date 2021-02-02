using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WallQuizzManager : MonoBehaviour
{
  // VARIABLES DECLARATION AND INITIALIZATION

  public int state = 0;

  public List<QuestionAndAnswers> QnA;
  public List<IntroductionQuizz> IntroQuizz;
  public List<Feedback> Feedback;
  public List<EndGame> EndGame;
  private int sizeofEndGame;
  int fb = 0;
  private int sizeofIntro;
  public GameObject[] options;

  public int currentQuestion = 0;   // index of the current question
  int totalQuestions = 0;   // total number of questions
  public Text QuestionTxt;    // text of the current question
  public int[] score = new int[4]; // array contening the score of each team
  public Text[] ScoreTxt = new Text[4];   // array of score in text of each team
  public bool[] alreadyAnswered = new bool [4];
  public string IntroductionTxt; // Text to introduce the activity (said by Leo)

  // Panels
  public GameObject QuestionsPanel;
  public GameObject PartialResultsPanel;
  public GameObject FinalResultsPanel;
  public GameObject UIElements;

  //From FloorQuizzManager
  public GameObject[] Floor; // to be linked to all the FloorQuizzManager scripts used

  //From TimeLeft
  public GameObject TimeLeft;

  // GameObject to control
  public GameObject LeoCharacter;
  public GameObject LeoText;
  public GameObject DialogueBalloon;



  // METHODS

  private void Start()
  {
    Debug.Log("Start WallQuizzManager");

    // Set the different Panels
    QuestionsPanel.SetActive(false);
    PartialResultsPanel.SetActive(false);
    FinalResultsPanel.SetActive(false);
    UIElements.SetActive(true);

    // Length of the Introduction
    sizeofIntro = IntroQuizz.Count;

    // Length of the End game
    sizeofEndGame = EndGame.Count;

    // Introduction of the activity
    StartCoroutine(IntroductionQuizz());

  }


  // Generate a question from the information provided in the Unity Inspector
  void generateQuestion()
  {
    Debug.Log("generateQuestion");
    Debug.Log("currentQuestion = " + currentQuestion);

    //Re-initialization of the alreadyAnswered array
    for(int k = 0 ; k < alreadyAnswered.Length ; k++){
      alreadyAnswered[k] = false;
      //Debug.Log("State " + k + " = " + alreadyAnswered[k]);
    }

    TimeLeft.GetComponent<TimeLeft>().StartTimer();

      // Set the question + answer text on the wall
      QuestionTxt.text = QnA[currentQuestion].Question;
      SetAnswersWall();

      // + set the helicopter on the wall ?? => another panel ? do it with another method

      // Set the answers on the floor
      for(int i=0; i < Floor.Length ; i++)
        {
          //Debug.Log("hey SetAnswersFloor" + i);
          Floor[i].GetComponent<FloorQuizzManager>().SetAnswersFloor(); // Set the answers of each FloorQuizzManager
        }

  }



  // Manage the answers displayed on the Wall
  public void SetAnswersWall()
  {
    for(int i = 0; i < options.Length; i++)
    {
      // Set the answer corresponding to the index of the current question, and putting it into the
      // corresponding Text component (= corresponding text bubble)
      options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Answers[i];
    }
  }



  // Display the intermediate results
  public void IntermediateResults()
  {
    Debug.Log("IntermediateResults");
    QuestionTxt.text = "Ecco i risultati per questa domanda :";

    // Panels
    QuestionsPanel.SetActive(false);
    PartialResultsPanel.SetActive(true);


    // Display the different scores
    for(int i=0 ; i < score.Length ; i++)
    {
      ScoreTxt[i].text = (score[i]).ToString();
    }

    // Wait for 10 seconds to display the scores
    StartCoroutine(waitForNext(10));

    // On to the next question (on the currentQuestion index)
    currentQuestion += 1;

  }



  // Display the final results
  public void FinalResults()
  {
    Debug.Log("FinalResults");

    // Panels
    QuestionsPanel.SetActive(false);
    PartialResultsPanel.SetActive(true);

    // Display the different scores
    for(int i=0 ; i < score.Length ; i++)
    {
      ScoreTxt[i].text = (score[i]).ToString();
    }

    StartCoroutine(LeoEndGame());

    Debug.Log("END OF GAME");

  }

  IEnumerator LeoEndGame()
  {
    for(int i = 0 ; i < sizeofEndGame ; i++)
    {
      QuestionTxt.text = EndGame[i].EndGameText;
      yield return new WaitForSeconds(8);
    }

    //Magic Room commands
    //MagicRoomManager.Instance.MagicRoomLightManager.SetColor(Color.green);
    //MagicRoomManager.Instance.MagicRoomAppliancesManager.SendChangeCommand("Macchina delle Bolle", "ON"); //turn on the bubble machine
    //yield return new WaitForSeconds(5);
    //MagicRoomManager.Instance.MagicRoomAppliancesManager.SendChangeCommand("Macchina delle Bolle", "OFF"); //turn off the bubble machine

    Debug.Log("Game Over, going to next level");

    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
  }


  IEnumerator waitForNext(int time)
  {
      yield return new WaitForSeconds(time);

      // Panels back to question mode
      QuestionsPanel.SetActive(true);
      PartialResultsPanel.SetActive(false);


      //Check if there are no more questions
      if(currentQuestion < totalQuestions)
      {
        // Generate the next question
        generateQuestion();
      }
      else
      {
        FinalResults();
      }
  }


  //Access the QnA
  public List<QuestionAndAnswers> GetQnA()
  {
    return QnA;
  }

/*
  IEnumerator waitForNextIntro(int time, int state){
    yield return new WaitForSeconds(time);

    if(state < 5)
    {
      Introduction();
    }
    else
    {
      //Set the different panels
      QuestionsPanel.SetActive(true);

      // initialize the variables
      totalQuestions = QnA.Count;   // total number of questions
      currentQuestion = 0;

      Debug.Log("Start WallQuizzManager Finished");

      // first method to generate all the questions
      generateQuestion();
    }

  }
*/

  //Introduction of the activity
  /*
  public void Introduction()
  {
      switch (state)
      {
        case 0:
          // Writes the text corresponding to the introduction
          IntroductionTxt = "Welcome to this activity ! My name is Leonardo Da Vinci, and I will guide you all along this activity.";
          QuestionTxt.text = IntroductionTxt;

          //Fade the different GameObjects
          //LeoCharacter.GetComponent<FadeIn>().StartFadingIn();
          //DialogueBalloon.GetComponent<FadeIn>().StartFadingIn();
          LeoText.GetComponent<TextFadeScript>().TextFadeIn();
          LeoText.GetComponent<TextFadeScript>().TextFadeOut();
          break;
        case 1:
          // Writes the text corresponding to the introduction
          IntroductionTxt = "Here are the rules to play : you have to organize in 4 teams, with one chief for each team.";
          QuestionTxt.text = IntroductionTxt;
          LeoText.GetComponent<TextFadeScript>().TextFadeIn();
          LeoText.GetComponent<TextFadeScript>().TextFadeOut();
          break;
        case 2:
          // Writes the text corresponding to the introduction
          IntroductionTxt = "For each question I will ask, the chief has to speak with his team, and then step on the chosen answer.";
          QuestionTxt.text = IntroductionTxt;
          LeoText.GetComponent<TextFadeScript>().TextFadeIn();
          LeoText.GetComponent<TextFadeScript>().TextFadeOut();
          break;
        case 3:
          // Writes the text corresponding to the introduction
          IntroductionTxt = "The goal is to answer as many correct answers as possible. I will announce the winners at the end of the activity.";
          QuestionTxt.text = IntroductionTxt;
          LeoText.GetComponent<TextFadeScript>().TextFadeIn();
          LeoText.GetComponent<TextFadeScript>().TextFadeOut();
          break;
        case 4:
          // Writes the text corresponding to the introduction
          IntroductionTxt = "Good luck to everyone ! And let's start !";
          QuestionTxt.text = IntroductionTxt;
          LeoText.GetComponent<TextFadeScript>().TextFadeIn();
          LeoText.GetComponent<TextFadeScript>().TextFadeOut();
          break;
        default:
          break;
      }
      StartCoroutine(waitForNextIntro(5, state));
      state += 1;
      Debug.Log("Hello " + state);
  }
  */
  IEnumerator IntroductionQuizz()
  {
    Debug.Log("Start Intro");
    // Leo will explain each step of the activity, it is possible to add as many introudction steps
    //  as we want in the GameManager
    for(int i = 0 ; i < sizeofIntro ; i++)
    {
      Debug.Log("Intro Text n°" + i);
      QuestionTxt.text = IntroQuizz[i].IntroductionQuizzText;
      yield return new WaitForSeconds(1);
    }

    Debug.Log("Finish Intro");

    //Set the different panels
    QuestionsPanel.SetActive(true);

    // initialize the variables
    totalQuestions = QnA.Count;   // total number of questions
    currentQuestion = 0;

    Debug.Log("Start WallQuizzManager Finished");

    // first method to generate all the questions
    generateQuestion();

  }


  // After each question, when the time is out
  public void TimeIsOut()
  {
    IntroductionTxt = "Il tempo è scaduto !";
    QuestionTxt.text = IntroductionTxt;
    LeoText.GetComponent<TextFadeScript>().TextFadeIn();
    LeoText.GetComponent<TextFadeScript>().TextFadeOut();

/*  // Magic Room controls
    MagicRoomManager.Instance.MagicRoomLightManager.SetColor(Color.red);
    MagicRoomManager.Instance.MagicRoomTextToSpeechManager.GenerateAudioFromText("stringtoRead", MagicRoomManager.Instance.MagicRoomTextToSpeechManager.ListOfVoices.ToArray()[0]);
    MagicRoomManager.Instance.MagicRoomAppliancesManager.SendChangeCommand("Macchina delle Bolle", "ON"); //turn on the bubble machine
    MagicRoomManager.Instance.MagicRoomAppliancesManager.SendChangeCommand("Macchina delle Bolle", "OFF"); //turn off the bubble machine
*/
    StartCoroutine(waitForNextTimeIsOut(5));
  }

  IEnumerator waitForNextTimeIsOut(int time)
  {
    yield return new WaitForSeconds(time);

    StartCoroutine(LeoFeedback());
  }

  IEnumerator LeoFeedback()
  {
    QuestionTxt.text = Feedback[fb].FeedbackText;
    yield return new WaitForSeconds(8);

    fb++;

    IntermediateResults();
  }


}
