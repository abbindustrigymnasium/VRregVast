using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This code in an example on how to turn on a sound and was programmed by Simon Meier,
//It access the TriggerSound in AudioManager.cs and inputs a name of the sound to it, it then handels the rest. 

//Everything encapulated by /// is needed. 

public class PlayAudioExample : MonoBehaviour
{
    /// This is needed in order to access the audioManager. 
    private Audio audioManager;
    /// 

    void Start()
    {
        ///This can of course be encapsulated in if statements and other stuff so that you can play the sound whenever you want.
        audioManager = FindObjectOfType<Audio>();
        if (audioManager != null)
        {
            //In "Monkey" you write the name the sound was given in the AudioManager array
            audioManager.TriggerSound("Bakgrund");
            audioManager.TriggerSound("BakgrundSug");
        }
        ///

    }
}
