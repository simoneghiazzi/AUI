using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class HollowsAnimationScript : MonoBehaviour
{
    [SerializeField]
    private Transform Lhollow, Rhollow;
    private float rotationSpeed, rotationTime;

    //Timer to change rotation each X seconds
    private Timer timer = new Timer();

    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = 0.2f;
        rotationTime = 2000f;
        timer.Interval = rotationTime;
        timer.Elapsed += ChangeRotation;
        timer.Start();
    }

    // Update is called once per frame
    void Update()
    {
        Lhollow.transform.Rotate(Vector3.back * rotationSpeed);
        Rhollow.transform.Rotate(-Vector3.back * rotationSpeed);
    }

    void ChangeRotation(object o, System.EventArgs e)
    {
        timer.Stop();

        rotationSpeed = -rotationSpeed;

        if (rotationTime < 3500f)
        {
            rotationTime += 600f;
        }
        timer.Interval = rotationTime;
        timer.Start();
    }

    public void CollisionReset()
    {
        Lhollow.transform.rotation = Quaternion.Euler(0, 0, 90);
        Rhollow.transform.rotation = Quaternion.Euler(0, 0, -90);
    }
}
