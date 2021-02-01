using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Subjects : MonoBehaviour
{
    public GameObject MenuManager;

    bool buttonPressed = false;


    public IEnumerator StartSubjects()
    {
      MenuManager.GetComponent<MenuManager>().Subjects.SetActive(true);

      MenuManager.GetComponent<MenuManager>().LeoText.text = MenuManager.GetComponent<MenuManager>().GetMenuText()[0].SubjectText[0];
      yield return new WaitForSeconds(1);
    }

    public void Update()
    {
      if(buttonPressed == true)
      {
        MenuManager.GetComponent<MenuManager>().step++;

        MenuManager.GetComponent<MenuManager>().PanelManager();
      }
    }


    public void DaVinciButton()
    {
      buttonPressed = true;

    }

}
