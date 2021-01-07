using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System;
using System.Diagnostics;
using System.Threading;
using System;
using System.Diagnostics;

public class CollisionsScript : MonoBehaviour
{
    //Variables used for making the boat blink after a collision
    public float spriteBlinkingTimer = 0.0f;
    public float spriteBlinkingMiniDuration = 0.1f;
    public float spriteBlinkingTotalTimer = 0.0f;
    public float spriteBlinkingTotalDuration = 2.0f;
    public bool startBlinking = false;

    //Variables used to kepp track of the race time
    private Stopwatch stopwatch;
    public double timePassed;

    private Vector3 initialPos;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
        stopwatch = new Stopwatch();
        stopwatch.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (startBlinking == true)
        {
            SpriteBlinkingEffect();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Pier")
        {
            transform.position = initialPos;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            startBlinking = true;
            
            //To reset and disable rudder after collision
            gameObject.GetComponent<RudderScript>().wheelBeingHeld = false;
            gameObject.GetComponent<RudderScript>().wheelAngle = 0f;
            gameObject.GetComponent<RudderScript>().enabled = false;
        }
        else if (other.gameObject.name == "Ldoor" || other.gameObject.name == "Rdoor")
        {
            timePassed = stopwatch.Elapsed.TotalSeconds;
            stopwatch.Stop();

            UnityEngine.Debug.Log("Time: " + timePassed);
        }
    }

    private void SpriteBlinkingEffect()
    {
        spriteBlinkingTotalTimer += Time.deltaTime;
        if (spriteBlinkingTotalTimer >= spriteBlinkingTotalDuration)
        {
            startBlinking = false;
            spriteBlinkingTotalTimer = 0.0f;
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            gameObject.GetComponent<RudderScript>().enabled = true;
            return;
        }

        spriteBlinkingTimer += Time.deltaTime;
        if (spriteBlinkingTimer >= spriteBlinkingMiniDuration)
        {
            spriteBlinkingTimer = 0.0f;
            if (this.gameObject.GetComponent<SpriteRenderer>().enabled == true)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }
}
