using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class LateralMovementScript : MonoBehaviour
{
    //Timer for randomly move the boat. Enums for setting the movement. Rnd variable to pick a random direction
    private Timer timer = new Timer();
    private enum Direction { RIGHT, LEFT, NONE }
    private Direction direction;
    private int rnd;

    //Vector to be used for the random rotation and movement of the boat
    private Vector3 rndVector;

    //The greater the slower the boat translates. Retrieved from RudderScript
    private float waterResistance;

    // Start is called before the first frame update
    void Start()
    {
        waterResistance = GetComponent<RudderScript>().waterResistance;

        direction = Direction.NONE;
        timer.Interval = 5000f;
        timer.Elapsed += SetDirection;
        timer.Start();

        rndVector = new Vector3(0.0f, 0.0f, 0.06f);
    }

    // Update is called once per frame
    void Update()
    {
        switch (direction)
        {
            case Direction.LEFT:
                transform.Rotate(rndVector);
                transform.Translate(-transform.rotation.z / waterResistance, 0, 0, Space.World);
                break;
            case Direction.RIGHT:
                transform.Rotate(-rndVector);
                transform.Translate(-transform.rotation.z / waterResistance, 0, 0, Space.World);
                break;
            case Direction.NONE:
                break;
            default:
                break;
        }
    }

    void SetDirection(object o, System.EventArgs e)
    {
        timer.Stop();

        UnityThread.executeInUpdate(() =>
        {
            rnd = UnityEngine.Random.Range(0, 2);
        });

        switch (rnd)
        {
            case 0:
                direction = Direction.LEFT;
                break;
            case 1:
                direction = Direction.RIGHT;
                break;
            default:
                break;
        }

        timer.Start();
    }
}
