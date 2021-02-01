using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Topics : MonoBehaviour
{
    public GameObject MenuManager;

    bool InvenzioniPressed = false;
    bool IniziaPressed = false;


    public IEnumerator StartTopics()
    {
      MenuManager.GetComponent<MenuManager>().Topics.SetActive(true);
      //Set the bubbles
      MenuManager.GetComponent<MenuManager>().LeoText.text = MenuManager.GetComponent<MenuManager>().GetMenuText()[0].DaVinciTopicsText[0];
      yield return new WaitForSeconds(1);
    }

    public void Update()
    {
      if(InvenzioniPressed == true)
      {
        if(IniziaPressed == true)
        {
          MenuManager.GetComponent<MenuManager>().step++;

          MenuManager.GetComponent<MenuManager>().PanelManager();
        }
      }
    }

    public void InvenzioniButton()
    {
      InvenzioniPressed = true;
      MenuManager.GetComponent<MenuManager>().LeoText.text = MenuManager.GetComponent<MenuManager>().GetMenuText()[0].DaVinciTopicsText[1];

      MenuManager.GetComponent<MenuManager>().IniziaButton.SetActive(true);
    }

    public void IniziaBtn()
    {
      IniziaPressed = true;

    }



}
