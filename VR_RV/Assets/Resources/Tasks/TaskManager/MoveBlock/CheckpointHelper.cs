using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointHelper : MonoBehaviour
{
    private GameObject task_manager;
    private void Start()
    {
        task_manager = GameObject.Find("Task Manager");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "BLOCK")
        {
            MoveBlockTaskStep step = task_manager.GetComponentInChildren<MoveBlockTaskStep>();
            if (step.id == "move")
            {
                step.Trigger();
            }
        }
    }
}
