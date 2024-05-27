using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class TaskStep : MonoBehaviour
{
    private bool is_finished = false;

    private string task_id;
    private int step_index;

    public void Initialize_Task_Step(string task_id, int step_index, string task_step_state)
    {
        this.task_id = task_id;
        this.step_index = step_index;
        if (task_step_state != null && task_step_state != "")
        {
            Set_Task_Step_State(task_step_state);
        }
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

    protected void Change_State(string new_state)
    {
        EventsManager.instance.task_events.Task_Step_State_Change(task_id, step_index, new TaskStepState(new_state));
    }

    protected abstract void Set_Task_Step_State(string state);
}