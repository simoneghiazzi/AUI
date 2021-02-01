using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Modalities : MonoBehaviour
{
    public GameObject MenuManager;

    public Collider Multipla;


    public void StartModalities()
    {
      //Set the bubbles
      MenuManager.GetComponent<MenuManager>().Singola.SetActive(true);
      MenuManager.GetComponent<MenuManager>().SingolaInfoBubble.SetActive(false);
      MenuManager.GetComponent<MenuManager>().Multipla.SetActive(true);
      MenuManager.GetComponent<MenuManager>().MultiplaInfoBubble.SetActive(false);

      MenuManager.GetComponent<MenuManager>().LeoText.text = MenuManager.GetComponent<MenuManager>().GetMenuText()[0].ModalitiesText[0];

    }

    //MULTIPLA BUTTON
    public void OnTriggerEnter(Collider Multipla)
    {
      Debug.Log(" Player : Multipla selected, wait 2 seconds for validation");

      StartCoroutine(waitForValidation());
    }
    IEnumerator waitForValidation()
    {
      yield return new WaitForSeconds(2);

      Debug.Log("Answer validated");

      MenuManager.GetComponent<MenuManager>().Singola.SetActive(false);
      MenuManager.GetComponent<MenuManager>().Multipla.SetActive(false);

      MenuManager.GetComponent<MenuManager>().step++;

      MenuManager.GetComponent<MenuManager>().PanelManager();
    }
    public void OnTriggerExit(Collider Multipla)
    {
      Debug.Log("Multipla deselected");
    }


}
