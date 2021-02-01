using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class BoatManagerScript : MonoBehaviour
{
    //We just check the rotation of one of the doors to see if it has been opened or closed
    private Transform firstLeftDoor, secondLeftDoor, deepWater;

    //The alpha value of the deep water
    private float alpha = 1.0f;

    //Boolean to check if we have passed the first door
    public bool firstStep = false;

    //Boolean to check if we have closed the first door
    public bool secondStep = false;

    //Boolean to check if the boat has reached the goal
    public bool thirdStep = false;

    //Last boolean, to stop increasing the score
    public bool fourthStep= false;

    //Variables used to keep track of the race time
    private Stopwatch stopwatch;
    public double timePassed;

    [SerializeField]
    private GameObject water, firstDoor, secondDoor;

    // Start is called before the first frame update
    void Start()
    {
        firstLeftDoor = firstDoor.transform.GetChild(0);
        secondLeftDoor = secondDoor.transform.GetChild(0);
        deepWater = water.transform.GetChild(0);

        stopwatch = new Stopwatch();
        stopwatch.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if(firstLeftDoor.rotation.eulerAngles.z >= 75 && !firstStep && !secondStep)
        {
            water.GetComponent<AutomaticWaterScript>().enabled = true;
            firstDoor.GetComponent<DoorScript>().toOpen = false;
            firstDoor.GetComponent<DoorScript>().wheelAngle = 0.0f;
            firstDoor.GetComponent<DoorScript>().enabled = false;
            firstDoor.GetComponent<GesturesDoorScript>().enabled = false;
            gameObject.GetComponent<HollowsAnimationScript>().enabled = true;

            transform.Translate(0, 0.1f, 0, Space.World);
        }
        else if(firstLeftDoor.rotation.eulerAngles.z <=76 && firstStep) //The check on the angle is due to the fact that Euler Angles range from 0 to 360
        {
            water.GetComponent<AutomaticWaterScript>().enabled = false;
            gameObject.GetComponent<HollowsAnimationScript>().enabled = false;
            firstDoor.GetComponent<DoorScript>().enabled = true;
        }
        else if (firstLeftDoor.rotation.eulerAngles.z >= 76 && firstStep) //The check on the angle is due to the fact that Euler Angles range from 0 to 360
        {
            firstDoor.GetComponent<DoorScript>().enabled = false;
            firstDoor.GetComponent<DoorScript>().closeDoors();
            deepWater.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, alpha);
            alpha -= 0.002f;
            if (alpha <= 0.0)
            {
                firstStep = false;
                secondStep = true;
                secondDoor.GetComponent<DoorScript>().enabled = true;
            }
        }

        if(secondLeftDoor.rotation.eulerAngles.z >= 75 && secondStep && !thirdStep && !fourthStep)
        {
            water.GetComponent<AutomaticWaterScript>().enabled = true;
            secondDoor.GetComponent<DoorScript>().toOpen = false;
            secondDoor.GetComponent<DoorScript>().wheelAngle = 0.0f;
            secondDoor.GetComponent<DoorScript>().enabled = false;
            gameObject.GetComponent<HollowsAnimationScript>().enabled = true;
        }
        else if (secondLeftDoor.rotation.eulerAngles.z <= 76 && thirdStep && !fourthStep)
        {
            water.GetComponent<AutomaticWaterScript>().enabled = false;
            gameObject.GetComponent<HollowsAnimationScript>().enabled = false;

            timePassed = stopwatch.Elapsed.TotalSeconds;
            stopwatch.Stop();

            //We save the time in the game manager and said that this boat has reached the goal
            if (gameObject.name == "Lboat")
            {
                GameManager.instance.score1 += timePassed;
                GameManager.instance.firstDone = true;
            }
            else if (gameObject.name == "Rboat")
            {
                GameManager.instance.score2 += timePassed;
                GameManager.instance.secondDone = true;
            }

            fourthStep = true;
        }
    }
}
