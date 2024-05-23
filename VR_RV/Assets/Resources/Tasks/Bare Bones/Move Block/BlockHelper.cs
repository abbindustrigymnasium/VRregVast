using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockHelper : MonoBehaviour
{
    private GameObject task_manager;
    private void Start()
    {
        task_manager = GameObject.Find("Task Manager");
    }
    public void PickedUp()
    {
        MoveBlockTaskStep step = task_manager.GetComponentInChildren<MoveBlockTaskStep>();
        if (step.id == "pickup")
        {
            step.Trigger();
        }
    }

    public void Dropped()
    {
        MoveBlockTaskStep step = task_manager.GetComponentInChildren<MoveBlockTaskStep>();
        if (step.id == "drop")
        {
            step.Trigger();
        }
    }
}
