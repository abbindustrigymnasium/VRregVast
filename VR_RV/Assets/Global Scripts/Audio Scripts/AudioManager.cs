using UnityEngine.Audio;
using UnityEngine;
using System;

public class Audio : MonoBehaviour
{

    public Sound[] sounds;


    //When starting it creates sound sources for each sound in the sounds array.
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

    }

    //Here we play the sound, If you want to set it up on anothere script ask Simon Meier or watch this video:
    //https://www.youtube.com/watch?v=6OT43pvUyfY&ab_channel=Brackeys
    // At the time 10:14
    void Start()
    {
        Play("Monkey");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
        }
        s.source.Play();
    }
}
