
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class ExampleClass : MonoBehaviour
{
    void OnJointBreak(float breakForce)
    {
        Debug.Log("A joint has just been broken!, force: " + breakForce);
        Debug.Log("We could add that you have to reload to retry or remove the break part completly.");
    }
}
