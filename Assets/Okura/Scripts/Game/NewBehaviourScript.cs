using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField]
    VideoPlayer video;

    void Awake() 
    {
        video = this.GetComponent<VideoPlayer>();
        video.SetDirectAudioVolume(0, PlayerPrefs.GetFloat("BGMValue", 0.434f));
    }

    public void OnValueChange(float newSliderValue)
    {
        video.SetDirectAudioVolume(0, PlayerPrefs.GetFloat("BGMValue", 0.434f));
    }
}