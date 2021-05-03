using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GesturesDoorScript : MonoBehaviour
{
    [SerializeField]
    private Transform hand, shoulder;

    private float wheelAngle;

    private float circularAngle, radius;
    // Start is called before the first frame update
    void Start()
    {
        radius = hand.position.y - shoulder.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        //The following three lines (plus the declarations of the variables used for them) are just to simulate hand movement and check that code works
        /*circularAngle += Time.deltaTime;
        var offset = new Vector2(Mathf.Sin(circularAngle), Mathf.Cos(circularAngle)) * radius;
        hand.position = (Vector2)shoulder.position + offset;*/

        var tan = (hand.position.x - shoulder.position.x) / (hand.position.y - shoulder.position.y);

        //These nested if are due to the fact that Atan is between -Pi/2 and Pi/2
        //but we want it to range between -2Pi and 2Pi for increasing the roation range 
        if (hand.position.y < shoulder.position.y)
        {
            if (hand.position.x > shoulder.position.x)
            {
                wheelAngle = 180 + (float)(Math.Atan(tan) * (180 / Math.PI));
            }
            else
            {
                wheelAngle = -180 + (float)(Math.Atan(tan) * (180 / Math.PI));
            }
        }
        else
        {
            wheelAngle = (float)(Math.Atan(tan) * (180 / Math.PI));
        }

        gameObject.GetComponent<DoorScript>().wheelAngle = wheelAngle;
    }
}
