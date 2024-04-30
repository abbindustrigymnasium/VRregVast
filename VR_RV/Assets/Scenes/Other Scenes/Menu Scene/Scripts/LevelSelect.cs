using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelSelect : MonoBehaviour
{
    public TMP_Text text_gameobject;

    public void Övning()
    {
        text_gameobject = GetComponent<TMP_Text>();
        text_gameobject.text = "Vill du starta Övning?";
    }

    public void Prov()
    {
        text_gameobject = GetComponent<TMP_Text>();
        text_gameobject.text = "Vill du starta Prov?";
    }
}
