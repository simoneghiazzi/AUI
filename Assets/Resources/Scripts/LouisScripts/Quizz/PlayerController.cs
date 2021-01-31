using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
      Player.SetActive(false);
    }

    public void VirtualTeamsPlay()
    {
      Player.SetActive(true);
    }

    public void VirtualTeamsStop()
    {
      Player.SetActive(false);
    }
}
