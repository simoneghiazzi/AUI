using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class WaterScript : MonoBehaviour
{
    //Timer for randomly let water flow. Enums for setting the speed. Rnd variable to pick a random speed
    private Timer timer = new Timer();
    private enum Speed { STOP,SLOW, MEDIUM, FAST }
    private Speed speed;
    private int rnd;

    // Start is called before the first frame update
    void Start()
    {
        speed = Speed.STOP;
        timer.Interval = 2500f;
        timer.Elapsed += SetSpeed;
        timer.Start();
    }

    // Update is called once per frame
    void Update()
    {
        //To simulate the natural water current
        transform.Translate(0, 0.05f, 0, Space.World);

        switch (speed)
        {
            case Speed.STOP:
                break;
            case Speed.SLOW:
                transform.Translate(0, -0.1f, 0, Space.World);
                break;
            case Speed.MEDIUM:
                transform.Translate(0, -0.3f, 0, Space.World);
                break;
            case Speed.FAST:
                transform.Translate(0, -0.5f, 0, Space.World);
                break;
            default:
                break;
        }
    }

    void SetSpeed(object o, System.EventArgs e)
    {
        timer.Stop();

        UnityThread.executeInUpdate(() =>
        {
            rnd = UnityEngine.Random.Range(0, 4);
        });

        switch (rnd)
        {
            case 0:
                speed = Speed.SLOW;
                break;
            case 1:
                speed = Speed.MEDIUM;
                break;
            case 2:
                speed = Speed.FAST;
                break;
            case 3:
                speed = Speed.STOP;
                break;
            default:
                break;
        }

        timer.Start();
    }
}