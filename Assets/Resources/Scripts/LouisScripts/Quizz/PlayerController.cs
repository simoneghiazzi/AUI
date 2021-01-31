using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void VirtualTeamsPlay()
    {
      anim.Play("Player2");
    }

    public void VirtualTeamsStop()
    {

    }
}
