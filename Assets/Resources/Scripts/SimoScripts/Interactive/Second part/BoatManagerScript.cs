using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField]
    private GameObject water, firstDoor, secondDoor;

    // Start is called before the first frame update
    void Start()
    {
        firstLeftDoor = firstDoor.transform.GetChild(0);
        secondLeftDoor = secondDoor.transform.GetChild(0);
        deepWater = water.transform.GetChild(0);
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
            alpha -= 0.005f;
            if (alpha <= 0.0)
            {
                firstStep = false;
                secondStep = true;
                secondDoor.GetComponent<DoorScript>().enabled = true;
            }
        }

        if(secondLeftDoor.rotation.eulerAngles.z >= 75 && secondStep)
        {

        }
    }
}
