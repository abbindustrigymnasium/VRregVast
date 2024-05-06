using UnityEngine;
using System.Collections.Generic;

public class turnOn : MonoBehaviour
{
    public bool bollean;
    public List<GameObject> gameObjectsToControl = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (bollean)
        {
            foreach (GameObject obj in gameObjectsToControl)
            {
                obj.SetActive(true);
            }
        }
    }
}
