using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Subjects : MonoBehaviour
{
    public GameObject MenuManager;

    public Collider DaVinci;


    public void StartSubjects()
    {
      //Set the bubbles
      MenuManager.GetComponent<MenuManager>().Subjects.SetActive(true);

      MenuManager.GetComponent<MenuManager>().LeoText.text = MenuManager.GetComponent<MenuManager>().GetMenuText()[0].SubjectText[0];
    }

    //DA VINCI BUBBLE
    public void OnTriggerEnter(Collider DaVinci)
    {
      Debug.Log(" Player : Da Vinci selected, wait 2 seconds for validation");

      StartCoroutine(waitForValidation());
    }
    IEnumerator waitForValidation()
    {
      yield return new WaitForSeconds(2);

      Debug.Log("Answer validated");

      MenuManager.GetComponent<MenuManager>().Subjects.SetActive(false);

      MenuManager.GetComponent<MenuManager>().step++;

      MenuManager.GetComponent<MenuManager>().PanelManager();
    }
    public void OnTriggerExit(Collider DaVinci)
    {
      Debug.Log("DaVinci deselected");
    }

}
