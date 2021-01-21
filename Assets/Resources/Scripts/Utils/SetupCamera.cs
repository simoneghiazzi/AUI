using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SetupCamera : MonoBehaviour
{

    public bool isFront;
    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    void Start()
    {
        if (GameSetting.instance)
        {
            int indexDisplay = isFront ? MagicRoomManager.instance.systemConfiguration.frontalScreen : MagicRoomManager.instance.systemConfiguration.floorScreen;
            cam.targetDisplay = indexDisplay;
        }
        else {
            cam.targetDisplay = isFront ? 0 : 1;
        }
    }
}
