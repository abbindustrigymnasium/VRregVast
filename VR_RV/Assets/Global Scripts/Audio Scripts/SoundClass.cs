using UnityEngine.Audio;
using UnityEngine;

//This code was programed by Simon Meier, and is a part of the Audio functions.

//Creates as Sound class, and makes it serializable
[System.Serializable]
public class Sound
{
    public bool play_sound;
    public string name;
    public AudioClip clip;

    [Range(0f, 2f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}


