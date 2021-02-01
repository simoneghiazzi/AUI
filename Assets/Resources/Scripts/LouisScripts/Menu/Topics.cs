using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Topics : MonoBehaviour
{
    public GameObject MenuManager;

    public Collider Invenzioni;

    public Collider Inizia;


    public void StartTopics()
    {
      //Set the bubbles

      MenuManager.GetComponent<MenuManager>().LeoText.text = MenuManager.GetComponent<MenuManager>().GetMenuText()[0].DaVinciTopicsText[0];
    }

    //INVENZIONI BUBBLE
    public void OnTriggerEnterInvenzioni(Collider Invenzioni)
    {
      Debug.Log(" Player : Invenzioni selected, wait 2 seconds for validation");

      StartCoroutine(waitForFirstValidation());
    }
    IEnumerator waitForFirstValidation()
    {
      yield return new WaitForSeconds(2);

      Debug.Log("Answer validated");

      MenuManager.GetComponent<MenuManager>().LeoText.text = MenuManager.GetComponent<MenuManager>().GetMenuText()[0].DaVinciTopicsText[1];

      MenuManager.GetComponent<MenuManager>().Topics.SetActive(false);
      MenuManager.GetComponent<MenuManager>().IniziaButton.SetActive(true);
    }
    public void OnTriggerExitInvenzioni(Collider Invenzioni)
    {
      Debug.Log("Invenzioni deselected");
    }

    //INIZIA BUTTON
    public void OnTriggerEnterInizia(Collider Inizia)
    {
      Debug.Log(" Player : Inizia selected, wait 2 seconds for validation");

      StartCoroutine(waitForSecondValidation());
    }
    IEnumerator waitForSecondValidation()
    {
      yield return new WaitForSeconds(2);

      Debug.Log("Answer validated");

      MenuManager.GetComponent<MenuManager>().IniziaButton.SetActive(false);

      MenuManager.GetComponent<MenuManager>().step++;

      MenuManager.GetComponent<MenuManager>().PanelManager();
    }
    public void OnTriggerExitInizia(Collider Inizia)
    {
      Debug.Log("Inizia deselected");
    }
}
