using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuControlls : MonoBehaviour
{
    public TMP_Text text_gameobject;

    public void Övning()
    {
        text_gameobject.text = "Vill du starta Övning?";
    }

    public void Prov()
    {
        text_gameobject.text = "Vill du starta Prov?";
    }

    public void Quit()
    {
        Application.Quit();
        // UnityEditor.EditorApplication.isPlaying = false;
    }
}
