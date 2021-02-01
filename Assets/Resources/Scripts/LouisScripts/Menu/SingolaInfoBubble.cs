using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingolaInfoBubble : MonoBehaviour
{
  public GameObject MenuManager;

  public Collider SglInfoBubble;

  //SINGOLA INFO BUBBLE
  public void OnTriggerEnter(Collider SglInfoBubble)
  {
    MenuManager.GetComponent<MenuManager>().SingolaInfoBubble.SetActive(true);
    MenuManager.GetComponent<MenuManager>().SingolaTextInfoBubble.text = MenuManager.GetComponent<MenuManager>().GetMenuText()[0].SingolaText[0];
  }
  public void OnTriggerExit(Collider SglInfoBubble)
  {
    MenuManager.GetComponent<MenuManager>().SingolaInfoBubble.SetActive(false);
  }
}
