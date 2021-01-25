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
    //We need to access the script attached to water in order to stop it after reaching the Conca
    [SerializeField]
    private GameObject water;

    //Variables used for making the boat blink after a collision
    public float spriteBlinkingTimer = 0.0f;
    public float spriteBlinkingMiniDuration = 0.1f;
    public float spriteBlinkingTotalTimer = 0.0f;
    public float spriteBlinkingTotalDuration = 2.0f;
    public bool startBlinking = false;

    //Variables used to keep track of the race time
    private Stopwatch stopwatch;
    public double timePassed;

    private Vector3 initialPos;

    //Boolean used in the blinking function (explained there)
    private bool endReached = false;

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
            
            //To reset boat position and disable rudder after collision
            gameObject.GetComponent<RudderScript>().wheelBeingHeld = false;
            gameObject.GetComponent<RudderScript>().wheelAngle = 0f;
            gameObject.GetComponent<RudderScript>().CollisionReset();
            gameObject.GetComponent<RudderScript>().enabled = false;

            //Disable Gestures for controlling the rudder and water movement
            gameObject.GetComponent<GesturesRudderScript>().enabled = false;
            gameObject.GetComponent<GesturesWaterScript>().enabled = false;

            //We also stop the water movement
            water.GetComponent<WaterScript>().enabled = false;

            //We stop the movement of the hollows as well
            gameObject.GetComponent<HollowsAnimationScript>().CollisionReset();
            gameObject.GetComponent<HollowsAnimationScript>().enabled = false;

        }
        else if (!endReached)
        {
            gameObject.GetComponent<RudderScript>().wheelBeingHeld = true;
            gameObject.GetComponent<RudderScript>().enabled = true;
            gameObject.GetComponent<GesturesRudderScript>().enabled = true;
            gameObject.GetComponent<GesturesWaterScript>().enabled = true;
            water.GetComponent<WaterScript>().enabled = true;
            gameObject.GetComponent<HollowsAnimationScript>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<RudderScript>().enabled = false;
            gameObject.GetComponent<RudderScript>().CollisionReset();
            gameObject.GetComponent<GesturesRudderScript>().enabled = false;
            gameObject.GetComponent<GesturesWaterScript>().enabled = false;
            water.GetComponent<WaterScript>().enabled = false;
            gameObject.GetComponent<HollowsAnimationScript>().enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Pier")
        {
            transform.position = initialPos;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            startBlinking = true;
            
            
        }
        else if (other.gameObject.name == "Ldoor" || other.gameObject.name == "Rdoor")
        {
            //In variable timePassed we save the time of the team
            timePassed = stopwatch.Elapsed.TotalSeconds;
            stopwatch.Stop();
            //UnityEngine.Debug.Log("Time: " + timePassed);

            //Reaching the goal means having finished the race, so we disable everything
            endReached = true;
        }
    }

    private void SpriteBlinkingEffect()
    {
        //Enveloped in the if just to avoid blinking just before reaching the goal 
        //(it would generate errors due to the fact that we destroy the scripts
        if (!endReached)
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
}
