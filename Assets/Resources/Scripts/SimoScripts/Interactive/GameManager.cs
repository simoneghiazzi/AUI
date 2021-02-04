using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //Keep track of the time of the two teams
    public double score1, score2;

    //Check if both boats have reached the goal in the first level
    public bool firstDone, secondDone;

    public bool firstComplete, secondComplete;

    void Awake()
    {
        MakeSingleton();
    }

    private void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(firstComplete)
        {
            firstComplete = false;
            firstDone = secondDone = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (secondComplete)
        {
            secondComplete = false;
            firstDone = secondDone = false;
            SceneManager.LoadScene(0);
        }
    }
}
