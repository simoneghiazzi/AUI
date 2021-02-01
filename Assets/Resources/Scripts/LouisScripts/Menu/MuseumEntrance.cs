using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuseumEntrance : MonoBehaviour
{
    public GameObject MenuManager;


    public void StartMuseumEntrance()
    {
      for(int i = 0 ; i < 3 ; i++)
      {
        MenuManager.GetComponent<MenuManager>().LeoText.text = MenuManager.GetComponent<MenuManager>().GetMenuText()[0].MuseumEntranceText[i];

        StartCoroutine(waitMuseumEntrance());
      }

      StartCoroutine(waitEndMuseumEntrance());
    }

    IEnumerator waitMuseumEntrance()
    {
      yield return new WaitForSeconds(10);

    }

    IEnumerator waitEndMuseumEntrance()
    {
      yield return new WaitForSeconds(2);

      EndMuseumEntrance();
    }

    //back to panel manager to go to the next step
    public void EndMuseumEntrance()
    {
      MenuManager.GetComponent<MenuManager>().step++;

      MenuManager.GetComponent<MenuManager>().PanelManager();
    }


}
