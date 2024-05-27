using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{

    [SerializeField] private AudioMixer audio_mixer;


    public void Set_Level(float slider_value)
    {
        audio_mixer.SetFloat("music_volume", Mathf.Log10(slider_value) * 20);

    }
}
