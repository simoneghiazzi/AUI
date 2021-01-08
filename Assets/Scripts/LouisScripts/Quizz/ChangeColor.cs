using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    //Reference to Sprite Renderer component
    private Renderer rend;
    private Renderer rend2;

    //Color value that we can set in Inspector, white by default
    [SerializeField]
    private Color colorToTurnTo = Color.white;
    private Color colorFinal = Color.red;
    [SerializeField]
    private Color colorAlmostFinished = Color.white;

    //Use this for initialization
    private void Start()
    {
      //Assign Renderer component to rend variable
      rend = GetComponent<Renderer>();
      rend2 = GetComponent<Renderer>();

      //Change sprite color to selected color
      rend.material.color = colorToTurnTo;
    }

    public void AlmostFinishedColor()
    {
      //Change sprite color to the almost finished selected color
      rend2.material.color = colorAlmostFinished;
    }

    public void FinalColor()
    {
      //Change sprite color to the final selected color
      rend.material.color = colorFinal;
    }

    public void ResetColor()
    {
      //Change sprite color to the reset selected color
      rend.material.color = colorToTurnTo;
    }

}
