using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GiveItems : MonoBehaviour
{
    public Transform Box1;
    public GameObject Box2;
    private int Request = 2;
    private GameObject child;

    public void Update()
    {
        child = Box1.GetChild(Request).gameObject;
    }

    public void On_Select_Entered_Snap(SelectEnterEventArgs args)
    {
        if (GameObject.ReferenceEquals(args.interactableObject, Box1.GetChild(Request).gameObject.GetComponent<XRGrabInteractable>()))
        {
            GetComponent<MeshRenderer>().material.color = Color.green;
            // TODO: update the score here;
            child.SetActive(false);
            Request--;
        }
        else
        {
            // GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }

    public void On_Select_Exited_Snap(SelectExitEventArgs args)
    {
        // GetComponent<MeshRenderer>().material.color = Color.white;
    }
}
