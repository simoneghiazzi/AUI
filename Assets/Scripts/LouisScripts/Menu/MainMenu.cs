using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayQuizz()
    {
        SceneManager.LoadScene("LouisScene");
    }

    public void PlayDragnDrop()
    {
      SceneManager.LoadScene("SimoScene");      
    }
}
