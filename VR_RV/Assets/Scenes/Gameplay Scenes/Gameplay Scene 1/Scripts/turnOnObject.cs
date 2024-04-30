using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnOn : MonoBehaviour
{

    public bool bollean;
    public GameObject gameObjectToControl;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (bollean == true)
        {
            gameObjectToControl.SetActive(true);
        }
    }
}
