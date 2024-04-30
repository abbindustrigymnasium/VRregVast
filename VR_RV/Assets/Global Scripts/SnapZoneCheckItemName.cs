using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SnapZoneCheckItemName : MonoBehaviour
{
    public XRBaseInteractable correct_item;

    public void OnSelectEnteredSnap(SelectEnterEventArgs args)
    {
        if (GameObject.ReferenceEquals(args.interactableObject, correct_item))
        {
            GetComponent<MeshRenderer>().material.color = Color.green;
            // TODO: update the score here;
        }
        else
        {
            GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }

    public void OnSelectExitedSnap(SelectExitEventArgs args)
    {
        GetComponent<MeshRenderer>().material.color = Color.white;
    }
}
