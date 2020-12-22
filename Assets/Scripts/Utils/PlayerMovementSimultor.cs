using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


public class PlayerMovementSimultor : MonoBehaviour
{

    public Skeleton skeleton;
    [Range(1,6)]
    public int id;

    private Animator anim;
    private Vector3 reversedstandardizedFloorSize;
    private Vector3 sensorDisallinment;


    void Start()
    {
        anim = GetComponent<Animator>();
        if (MagicRoomManager.instance.systemConfiguration != null)
        {
            sensorDisallinment = new Vector3(MagicRoomManager.instance.systemConfiguration.floorOffsetX, MagicRoomManager.instance.systemConfiguration.floorOffsetY, 0);
            Debug.Log(MagicRoomManager.instance.systemConfiguration.floorSizeX);
            Debug.Log(MagicRoomManager.instance.systemConfiguration.floorSizeY);
            reversedstandardizedFloorSize = new Vector3(MagicRoomManager.instance.systemConfiguration.floorSizeX * 0.065934f, 1, -MagicRoomManager.instance.systemConfiguration.floorSizeY* 0.125f);
        }
    }

    public void Setup(int id) { 
        this.id = id;
        Color col = Color.clear;
        switch (id) {
            case 1:
                col = Color.red;
                break;
            case 2:
                col = Color.blue;
                break;
            case 3:
                col = Color.green;
                break;
            case 4:
                col = new Color (1,1,0);
                break;
            case 5:
                col = new Color(1,0,1);
                break;
            case 6:
                col = new Color(0,1,1);
                break;
        }
        transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material.color = col;
        transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.color = col;
    }

    void Update()
    {
        updateSkeleton();
        if (Input.GetKey(id.ToString()))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                anim.SetTrigger("Jump");
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                anim.SetBool("Walk", true);
                transform.position += transform.forward * 0.05f;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                anim.SetBool("Walk", true);
                transform.position += transform.forward * -0.05f;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                anim.SetBool("Walk", true);
                transform.RotateAround(transform.position, Vector3.up, 1);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                anim.SetBool("Walk", true);
                transform.RotateAround(transform.position, Vector3.up, -1);
            }
            if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow)) {
                anim.SetBool("Walk", false);
            }
            if (Input.GetKeyDown(KeyCode.LeftShift)) {
                List<string> gestures = new List<string>();
                gestures.AddRange(skeleton.Gestures);
                gestures.Add("CLOSEHANDLEFT");
                skeleton.Gestures = gestures.ToArray();
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                List<string> gestures = new List<string>();
                gestures.AddRange(skeleton.Gestures);
                if (gestures.Contains("CLOSEHANDLEFT")){
                    gestures.Remove("CLOSEHANDLEFT");
                    skeleton.Gestures = gestures.ToArray(); 
                }
            }
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                List<string> gestures = new List<string>();
                gestures.AddRange(skeleton.Gestures);
                gestures.Add("CLOSEHANDRIGHT");
                skeleton.Gestures = gestures.ToArray();
            }
            if (Input.GetKeyUp(KeyCode.RightShift))
            {
                List<string> gestures = new List<string>();
                gestures.AddRange(skeleton.Gestures);
                if (gestures.Contains("CLOSEHANDRIGHT"))
                {
                    gestures.Remove("CLOSEHANDRIGHT");
                    skeleton.Gestures = gestures.ToArray();
                }
            }
        }
    }

    private void updateSkeleton()
    {
        foreach (Transform child in gameObject.GetComponentsInChildren<Transform>()) {
            Vector3 pos = child.position;
            pos = Vector3.Scale(pos, reversedstandardizedFloorSize);
            pos += Vector3.forward * 1.4f;
            pos = pos + sensorDisallinment;
            
            skeleton.SetPropertyValue(child.gameObject.name, pos);
        }
    }
}
