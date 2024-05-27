using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SnapZoneCheckItemName : MonoBehaviour
{
    public XRBaseInteractable correct_item;

    public ItemTaskManager item_task_manager;

    public void Start()
    {
        GameObject item_task_manager_go = GameObject.Find("Item Task Manager");

        if (item_task_manager_go != null)
        {
            item_task_manager = item_task_manager_go.GetComponent<ItemTaskManager>();
            item_task_manager.Add_Item();
        }
    }

    public void On_Select_Entered_Snap(SelectEnterEventArgs args)
    {
        if (GameObject.ReferenceEquals(args.interactableObject, correct_item))
        {
            GetComponent<MeshRenderer>().material.color = Color.green;
            // TODO: update the score here;
            if (item_task_manager != null) { item_task_manager.Correct(); }
        }
        else
        {
            GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }

    public void On_Select_Exited_Snap(SelectExitEventArgs args)
    {
        GetComponent<MeshRenderer>().material.color = Color.white;
    }
}
