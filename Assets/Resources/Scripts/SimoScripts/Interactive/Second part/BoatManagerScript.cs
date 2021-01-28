using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatManagerScript : MonoBehaviour
{
    //We just check the rotation of one of the doors to see if it has been opened or closed
    private Transform firstLeftDoor, secondLeftDoor;

    //Boolean to check if we have passed the first door
    public bool firstStep = false;

    [SerializeField]
    private GameObject water, firstDoor, secondDoor;

    // Start is called before the first frame update
    void Start()
    {
        firstLeftDoor = firstDoor.transform.GetChild(0);
        secondLeftDoor = secondDoor.transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(firstLeftDoor.rotation.eulerAngles.z >= 75 && !firstStep)
        {
            water.GetComponent<AutomaticWaterScript>().enabled = true;
            firstDoor.GetComponent<DoorScript>().toOpen = false;
            firstDoor.GetComponent<DoorScript>().wheelAngle = 0.0f;
            firstDoor.GetComponent<DoorScript>().enabled = false;
            gameObject.GetComponent<HollowsAnimationScript>().enabled = true;

            transform.Translate(0, 0.1f, 0, Space.World);
        }
        else if(firstStep)
        {
            water.GetComponent<AutomaticWaterScript>().enabled = false;
            gameObject.GetComponent<HollowsAnimationScript>().enabled = false;
            firstDoor.GetComponent<DoorScript>().enabled = true;
        }
    }
}
