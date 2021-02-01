using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    // VARIABLES & DECLARATION

    public List<MenuText> MenuText;

    public GameObject FrontMuseum;
    public GameObject MuseumEntrance;

    //UIElements
    public GameObject LeoCharacter;
    public GameObject LeoHappy;
    public GameObject LeoExplaining;
    public GameObject LeoTextBubble;
    public Text LeoText;

    //Modalities
    public GameObject Modalities;
    public GameObject Singola;
    public GameObject SingolaInfoButton;
    public GameObject SingolaInfoBubble;
    public Text SingolaTextInfoBubble;
    public GameObject Multipla;
    public GameObject MultiplaInfoButton;
    public GameObject MultiplaInfoBubble;
    public Text MultiplaTextInfoBubble;

    //Subjects
    public GameObject Subjects;
    public GameObject DaVinci;

    //Topics
    public GameObject Topics;
    public GameObject Invenzioni;

    //Inizia
    public GameObject IniziaButton;

    //Other variable
    public int step = 0; // to make the switch case


    // Start is called before the first frame update
    void Start()
    {
      Debug.Log("Start Menu");

      //INITIALIZATION OF THE MENU
      LeoHappy.SetActive(true);
      FrontMuseum.SetActive(true);

      //DEFAULT VALUES
      LeoExplaining.SetActive(false);
      MuseumEntrance.SetActive(false);
      LeoTextBubble.SetActive(false);
      Modalities.SetActive(false);
      Subjects.SetActive(false);
      Topics.SetActive(false);
      IniziaButton.SetActive(false);

      PanelManager();
      Debug.Log("Finish Start Menu");
    }


    public void PanelManager()
    {
      Debug.Log("Panel Manager Start");


      switch(step)
      {
        case 0:
          Debug.Log("Front of the Museum");

          StartCoroutine(FrontMuseum.GetComponent<FrontMuseum>().StartFrontMuseum());

          break;
        case 1:
          Debug.Log("Leo presentation");

          StartCoroutine(LeoCharacter.GetComponent<LeoPresentation>().StartLeoPresentation());



          break;
        case 2:
          Debug.Log("Museum Entrance");

          LeoHappy.SetActive(false);
          FrontMuseum.SetActive(false);
          MuseumEntrance.SetActive(true);

          StartCoroutine(MuseumEntrance.GetComponent<MuseumEntrance>().StartMuseumEntrance());


          break;
        case 3:
          Debug.Log("Modalities");

          Modalities.SetActive(true);

          StartCoroutine(Modalities.GetComponent<Modalities>().StartModalities());

          break;
        case 4:
          Debug.Log("Subjects");



          StartCoroutine(Subjects.GetComponent<Subjects>().StartSubjects());

          break;
        case 5:
          Debug.Log("Topics");

          Subjects.SetActive(false);

          StartCoroutine(Topics.GetComponent<Topics>().StartTopics());

          break;
        default:
          break;
      }


      if(step >= 6)
      {
        EndMenu();
      }
    }


    public void EndMenu()
    {
      Debug.Log("End of the menu, going to the next scene");
      //SceneManager.LoadScene("LouisScene");
    }

    public List<MenuText> GetMenuText()
    {
      return MenuText;
    }
}
