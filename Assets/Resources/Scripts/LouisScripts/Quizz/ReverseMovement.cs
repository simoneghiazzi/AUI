using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseMovement : MonoBehaviour
{
    [SerializeField] Transform player;
    private Transform myTrans;
    [SerializeField] float k = 1.8f;

    private void Start()
    {
       
        myTrans = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        float y = player.position.z;
        if (y > 2)
            y = y + y * (k / 3);
        if (y < 0)
        {
      
            y = y + y * k;
        }

        myTrans.position = new Vector3(player.position.x, y, myTrans.position.z);
    }
}
