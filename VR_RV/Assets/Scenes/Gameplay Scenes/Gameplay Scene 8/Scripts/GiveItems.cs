using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GiveItems : MonoBehaviour
{
    public Transform Box1;
    public GameObject Box2;
    private int Request = 0;
    private GameObject child;

    void Start()
    {
        ChildUpdate();
        if (Box1 == null)
        {
            Debug.LogError("Box1 is not assigned in the inspector.");
            return;
        }

        if (Box1.childCount == 0)
        {
            Debug.LogError("Box1 has no children.");
            return;
        }

        for (int i = 0; i < Box1.childCount; i++)
        {
            Debug.Log(Box1.GetChild(i).gameObject);
        }

        Debug.Log("Box1 is assigned and has " + Box1.childCount + " children.");
    }


    public void ChildUpdate()
    {
        child = Box1.GetChild(Request).gameObject;
    }

    public void On_Select_Entered_Snap(SelectEnterEventArgs args)
    {
        if (GameObject.ReferenceEquals(args.interactableObject, child.GetComponent<XRGrabInteractable>()))
        {
            //GetComponent<MeshRenderer>().material.color = Color.green;
            // TODO: update the score here;
            child.SetActive(false);
            Request++;
            ChildUpdate();
        }
        else
        {
            Debug.Log("Wrong Tool");
            // GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }

    public void On_Select_Exited_Snap(SelectExitEventArgs args)
    {
        // GetComponent<MeshRenderer>().material.color = Color.white;
    }
}
