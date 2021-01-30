using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject camera = GameObject.Find("Main Camera");

        // VideoPlayer automatically targets the camera backplane when it is added
        // to a camera object, no need to change videoPlayer.targetCamera.
        var videoPlayer = camera.AddComponent<UnityEngine.Video.VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
