using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoScript : MonoBehaviour
{
    private VideoPlayer video;

    // Start is called before the first frame update
    void Start()
    {
        var player = gameObject.transform.GetChild(0);
        video = player.gameObject.GetComponent<VideoPlayer>();
        video.loopPointReached += VideoEnded;
    }

    void VideoEnded(UnityEngine.Video.VideoPlayer vp)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
