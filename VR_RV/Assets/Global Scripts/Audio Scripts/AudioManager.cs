using UnityEngine.Audio;
using UnityEngine;
using System;
using UnityEngine.UI;

//This code was programed by Simon Meier, and is a part of the Audio functions.
//Note: Volume of clips cannot be changed when the simulation is running, only before in the editor.

public class Audio : MonoBehaviour
{

    public Sound[] sounds;
    public AudioMixerGroup audio_mixer_group;
    public AudioMixer audio_mixer;

    [SerializeField] private Slider volume_slider;



    //When starting it creates sound sources for each sound in the sounds array.
    void Awake()
    {
        foreach (Sound s in sounds)
        {

            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.outputAudioMixerGroup = audio_mixer_group;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

        }

    }

    private void Start()

    {

        volume_slider.onValueChanged.AddListener(Set_Level);

        if (PlayerPrefs.HasKey("music_volume"))
        {
            volume_slider.value = (float)Math.Pow(10, PlayerPrefs.GetFloat("music_volume") / 20);
        }
    }
    //Here we go through each sound in the array and check if we should play it.
    void Update()
    {
        foreach (Sound s in sounds)
        {
            if (s.play_sound)
            {
                Play(s.name);
                //It's important to note that the play_sound variable will be put to false as to not play the same sound twice.
                s.play_sound = false;
            }
        }
    }

    //This is the function that plays the sound using the name of the sound in the array.
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        s.source.Play();
    }

    //This is the part that other codes can access to turn play_sound to true. 
    public void TriggerSound(string soundName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        if (s != null)
        {
            s.play_sound = true;
        }
        else
        {
            Debug.LogWarning("Sound: " + soundName + " not found");
        }
    }

    public void Set_Level(float slider_value)
    {
        audio_mixer.SetFloat("music_volume", Mathf.Log10(slider_value) * 20);
        PlayerPrefs.SetFloat("music_volume", Mathf.Log10(slider_value) * 20);
    }

}
