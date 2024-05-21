using System;

public class TaskEvents
{
    public event Action<string> on_start_task;
    public void Start_Task(string id)
    {
        if (on_start_task != null)
        {
            on_start_task(id);
        }
    }

    public event Action<string> on_advance_task;
    public void Advance_Task(string id)
    {
        if (on_advance_task != null)
        {
            on_advance_task(id);
        }
    }

    public event Action<string> on_finish_task;
    public void Finish_Task(string id)
    {
        if (on_finish_task != null)
        {
            on_finish_task(id);
        }
    }

    public event Action<Task> on_task_state_change;
    public void Task_State_Change(Task task)
    {
        if (on_task_state_change != null)
        {
            on_task_state_change(task);
        }
    }

    // Not implemented yet

    // public event Action<string, int, TaskStepState> on_task_step_state_change;
    // public void Task_Step_State_Change(string id, int step_index, TaskStepState task_step_state)
    // {
    //     if (on_task_step_state_change != null)
    //     {
    //         on_task_step_state_change(id, step_index, task_step_state);
    //     }
    // }
}