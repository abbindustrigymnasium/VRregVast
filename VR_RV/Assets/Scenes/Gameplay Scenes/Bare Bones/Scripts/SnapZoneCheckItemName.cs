using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SnapZoneCheckItemName : MonoBehaviour
{
    public string correct_item_name;

    public bool CheckItemName(string itemName)
    {
        return itemName == correct_item_name;
    }

    public void OnSelectEntered(XRBaseInteractable interactable)
    {
        GameObject enteredObject = interactable.gameObject;
        string enteredObjectName = enteredObject.name;
        Debug.Log("Entered object name: " + enteredObjectName);
        if (CheckItemName(enteredObjectName))
        {
            GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
