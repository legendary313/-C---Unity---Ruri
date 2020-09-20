using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoIntroBehavior : MonoBehaviour
{
    public bool endVideo = false;
    VideoPlayer videoPlayer;
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        //Debug.Log(videoPlayer.frameCount);
    }
    void Update()
    {
        if (videoPlayer.frame < Convert.ToInt64(videoPlayer.frameCount) - 1)
        {
            endVideo = false;
            //Debug.Log(videoPlayer.frame);
        }
        else
        {
            videoPlayer.Stop();
            endVideo = true;
        }
        //Debug.Log(endVideo);
    }
}
