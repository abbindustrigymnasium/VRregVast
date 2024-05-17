using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public List<GameObject> spawned_tools;
    void Start()
    {
        foreach (GameObject tool in spawned_tools) {
            GameObject newObject = Instantiate(tool, transform.position, transform.rotation);
        }
    }
}
