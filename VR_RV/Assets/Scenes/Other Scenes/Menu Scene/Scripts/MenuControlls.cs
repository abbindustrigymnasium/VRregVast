using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuControlls : MonoBehaviour
{
    // Text object you want to change the text on
    public TMP_Text text_gameobject;

    // Changes a text to say "Vill du starta Övning?"
    public void Övning()
    {
        text_gameobject.text = "Vill du starta Övning?";
    }

    // Changes a text to say "Vill du starta Prov?"
    public void Prov()
    {
        text_gameobject.text = "Vill du starta Prov?";
    }

    // Closes down the game
    public void Quit()
    {
        Application.Quit();
        // UnityEditor.EditorApplication.isPlaying = false;
    }
}
