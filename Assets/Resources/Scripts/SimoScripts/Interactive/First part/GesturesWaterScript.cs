﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class GesturesWaterScript : MonoBehaviour
{
    [SerializeField]
    private Transform hand, shoulder;

    //Each time it elapses, we count the frequency of arm movement of the child
    private Timer timer = new Timer();

    //Boolean used to check that a child correctly moves his/her arm up and down
    private bool check = true;

    //Boolean set as true each time the timer elapses. The update of values must be done inside Unity's Update function
    private bool toUpdate = false;

    //Count of how many time arms are moved up and down before the timer elapses
    private int counter = 0;
    
    //Threshold for the height of the hand wrt the shoulder
    public float threshold = 60.0f;

    //Value multplied for the number of times a child correctly moves his/her arm in order to obtain the water speed
    private float basicSpeed;

    //Hand automatically moves to simulate gesture
    private float handMovement = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        basicSpeed = -5.0f;

        timer.Interval = 1000f;
        timer.Elapsed += SetSpeed;
        timer.Start();
    }

    // Update is called once per frame
    void Update()
    {
        //This Translate used to automatically move the end if Magic room not accessible 
        hand.Translate(0, handMovement, 0, Space.World);

        ///NOT A GOOD EFFECT FOR NOW
        //To simulate the natural water current
        //transform.Translate(0, 0.03f, 0, Space.World);

        if (hand.position.y > (shoulder.position.y + threshold) && check == true)
        {
            //Still for the automatic movement of the hand
            handMovement = -handMovement;

            counter += 1;
            check = false;
        }
        else if (hand.position.y < (shoulder.position.y - threshold) && check == false)
        {
            //Still for the automatic movement of the hand
            handMovement = -handMovement;

            check = true;
        }

        if(toUpdate)
        {
            transform.Translate(0, basicSpeed * counter, 0, Space.World);
            Debug.Log("POSIZIONE:"+ transform.position.y);
            counter = 0;
            toUpdate = false;
        }
    }

    void SetSpeed(object o, System.EventArgs e)
    {
        timer.Stop();

        Debug.Log("BASIC: " + basicSpeed);
        Debug.Log("COUNTER: " + counter);
        Debug.Log("Speed: " + basicSpeed * counter);

        toUpdate = true;

        timer.Start();
    }
}