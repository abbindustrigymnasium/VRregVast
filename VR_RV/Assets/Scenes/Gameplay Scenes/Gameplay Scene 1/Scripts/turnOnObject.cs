using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class turnOn : MonoBehaviour
{
    public bool bollean = false;
    private bool hasStartedCoroutine = false;

    public List<GameObject> gameObjectsToTurnOn = new List<GameObject>();
    public List<GameObject> gameObjectsToTurnOff = new List<GameObject>();

    void Update()
    {
        if (bollean && !hasStartedCoroutine)
        {
            StartCoroutine(TurnObjectsOn());
        }
    }

    IEnumerator TurnObjectsOn()
    {
        hasStartedCoroutine = true;
        yield return new WaitForSeconds(0.2f);

        foreach (GameObject obj in gameObjectsToTurnOff)
        {
            obj.SetActive(false);
        }

        foreach (GameObject obj in gameObjectsToTurnOn)
        {
            obj.SetActive(true);
        }


        hasStartedCoroutine = false;
        bollean = false;
    }
}
