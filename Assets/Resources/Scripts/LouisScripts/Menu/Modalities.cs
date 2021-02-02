using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Modalities : MonoBehaviour
{
    public GameObject MenuManager;
    bool buttonPressed = false;
    bool clickSingolaInfo = false;
    bool clickMultiplaInfo = false;


    public IEnumerator StartModalities()
    {
      Debug.Log("Start Modalities");

      MenuManager.GetComponent<MenuManager>().Modalities.SetActive(true);

      //Set the bubbles
      MenuManager.GetComponent<MenuManager>().Singola.SetActive(true);
      MenuManager.GetComponent<MenuManager>().SingolaInfoBubble.SetActive(false);
      MenuManager.GetComponent<MenuManager>().Multipla.SetActive(true);
      MenuManager.GetComponent<MenuManager>().MultiplaInfoBubble.SetActive(false);

      MenuManager.GetComponent<MenuManager>().LeoText.text = MenuManager.GetComponent<MenuManager>().GetMenuText()[0].ModalitiesText[0];

      yield return new WaitForSeconds(1);

    }

    public void Update()
    {
      if(buttonPressed == true)
      {
        MenuManager.GetComponent<MenuManager>().step++;
        buttonPressed = false;

        MenuManager.GetComponent<MenuManager>().PanelManager();
      }
      if(clickSingolaInfo == false){
        MenuManager.GetComponent<MenuManager>().SingolaInfoBubble.SetActive(false);
      }
      if(clickMultiplaInfo == false){
        MenuManager.GetComponent<MenuManager>().MultiplaInfoBubble.SetActive(false);
      }
    }



    public void SingolaInfoButton()
    {
      clickSingolaInfo = true;
      clickMultiplaInfo = false;
      Debug.Log("Displaying Singola Info Bubble");
      MenuManager.GetComponent<MenuManager>().SingolaInfoBubble.SetActive(true);
      MenuManager.GetComponent<MenuManager>().SingolaTextInfoBubble.text = MenuManager.GetComponent<MenuManager>().GetMenuText()[0].SingolaText[0];

    }

    public void MultiplaInfoButton()
    {
      clickMultiplaInfo = true;
      clickSingolaInfo = false;
      Debug.Log("Displaying Multiple Info Bubble");
      MenuManager.GetComponent<MenuManager>().MultiplaInfoBubble.SetActive(true);
      MenuManager.GetComponent<MenuManager>().MultiplaTextInfoBubble.text = MenuManager.GetComponent<MenuManager>().GetMenuText()[0].MultiplaText[0];
    }

    public void MultiplaButton()
    {
      buttonPressed = true;
      MenuManager.GetComponent<MenuManager>().Singola.SetActive(false);
      MenuManager.GetComponent<MenuManager>().Multipla.SetActive(false);
    }


}
