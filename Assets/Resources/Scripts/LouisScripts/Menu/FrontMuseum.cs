using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrontMuseum : MonoBehaviour
{

    public GameObject MenuManager;


    public IEnumerator StartFrontMuseum()
    {
      MenuManager.GetComponent<MenuManager>().LeoTextBubble.SetActive(true);

      for(int i = 0 ; i < 2 ; i++)
      {
        MenuManager.GetComponent<MenuManager>().LeoText.text = MenuManager.GetComponent<MenuManager>().GetMenuText()[0].FrontMuseumText[i];
        yield return new WaitForSeconds(1);
      }

      EndFrontMuseum();
    }


    //back to panel manager to go to the next step
    public void EndFrontMuseum()
    {
      MenuManager.GetComponent<MenuManager>().step++;
      MenuManager.GetComponent<MenuManager>().PanelManager();
    }

}
