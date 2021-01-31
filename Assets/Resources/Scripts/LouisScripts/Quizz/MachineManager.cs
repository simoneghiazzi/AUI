using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MachineManager : MonoBehaviour
{
  public GameObject Helicopter;
  public GameObject Ornithopter;
  public GameObject Parachute;
  public GameObject WallQuizzManager;

    // Start is called before the first frame update
    void Start()
    {
        Helicopter.SetActive(false);
        Ornithopter.SetActive(false);
        Parachute.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(WallQuizzManager.GetComponent<WallQuizzManager>().currentQuestion < 3)
        {
          Helicopter.SetActive(true);
          Ornithopter.SetActive(false);
          Parachute.SetActive(false);
        }
        if( (WallQuizzManager.GetComponent<WallQuizzManager>().currentQuestion > 2) && (WallQuizzManager.GetComponent<WallQuizzManager>().currentQuestion < 6) )
        {
          Helicopter.SetActive(false);
          Ornithopter.SetActive(true);
          Parachute.SetActive(false);
        }
        if(WallQuizzManager.GetComponent<WallQuizzManager>().currentQuestion > 5)
        {
          Helicopter.SetActive(false);
          Ornithopter.SetActive(false);
          Parachute.SetActive(true);
        }
    }
}
