﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GesturesRudderScript : MonoBehaviour
{
    [SerializeField]
    private Transform hand, shoulder;

    private float wheelAngle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //hand.Translate(-0.2f, -0.1f, 0, Space.World);

        var tan = (hand.position.x - shoulder.position.x) / (hand.position.y - shoulder.position.y);

        //These nested if are due to the fact that Atan is between -Pi/2 and Pi/2
        //but we want it to range between -2Pi and 2Pi for increasing the roation range 
        if(hand.position.y < shoulder.position.y)
        {
            if(hand.position.x > shoulder.position.x)
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
        
        gameObject.GetComponent<RudderScript>().wheelAngle = wheelAngle;
    }
}
