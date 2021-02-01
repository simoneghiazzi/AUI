using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplaInfoBubble : MonoBehaviour
{
  public GameObject MenuManager;

  public Collider MtplInfoBubble;

  //MULTIPLA INFO BUBBLE
  public void OnTriggerEnter(Collider MtplInfoBubble)
  {
    MenuManager.GetComponent<MenuManager>().MultiplaInfoBubble.SetActive(true);
    MenuManager.GetComponent<MenuManager>().MultiplaTextInfoBubble.text = MenuManager.GetComponent<MenuManager>().GetMenuText()[0].MultiplaText[0];
  }
  public void OnTriggerExitMtpl(Collider MtplInfoBubble)
  {
    MenuManager.GetComponent<MenuManager>().MultiplaInfoBubble.SetActive(false);
  }
}
