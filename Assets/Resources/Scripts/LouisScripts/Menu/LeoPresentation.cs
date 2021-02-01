using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeoPresentation : MonoBehaviour
{
  public GameObject MenuManager;


  public void StartLeoPresentation()
  {

    for(int i = 0 ; i < 1 ; i++)
    {
      MenuManager.GetComponent<MenuManager>().LeoText.text = MenuManager.GetComponent<MenuManager>().GetMenuText()[0].LeoPresentationText[i];

      StartCoroutine(waitLeoPresentation());
    }

    StartCoroutine(waitEndLeoPresentation());
  }

  IEnumerator waitLeoPresentation()
  {
    yield return new WaitForSeconds(10);
  }

  IEnumerator waitEndLeoPresentation()
  {
    yield return new WaitForSeconds(2);

    EndLeoPresentation();
  }

  public void EndLeoPresentation()
  {
    MenuManager.GetComponent<MenuManager>().step++;

    MenuManager.GetComponent<MenuManager>().PanelManager();
  }

}
