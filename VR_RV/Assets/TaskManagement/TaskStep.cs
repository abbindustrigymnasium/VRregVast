using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class TaskStep : MonoBehaviour
{
    private bool is_finished = false;

    private string task_id;

    public void Initialize_Task_Id(string task_id)
    {
        this.task_id = task_id;
    }

    protected void Finish_Step()
    {
        if (!is_finished)
        {
            is_finished = true;
            EventsManager.instance.task_events.Advance_Task(task_id);
            Destroy(this.gameObject);
        }

    }
}