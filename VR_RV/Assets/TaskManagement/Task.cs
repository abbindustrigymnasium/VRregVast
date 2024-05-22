using System;
using System.Collections.Generic;
using UnityEngine;

public class Task
{
    public TaskInfoSO info;

    public TaskState state;
    private int current_task_step_index;

    public Task(TaskInfoSO task_info)
    {
        this.info = task_info;
        this.state = TaskState.REQUIREMENTS_NOT_MET;
        this.current_task_step_index = 0;
    }

    public void Move_To_Next_Step()
    {
        current_task_step_index++;
    }

    public bool Current_Step_Exists()
    {
        return (current_task_step_index < info.task_step_prefabs.Length);
    }

    public void Instantiate_Current_Task_Step(Transform parent_transform)
    {
        GameObject task_step_prefab = Get_Current_Task_Step_Prefab();

        if (task_step_prefab != null)
        {
            TaskStep taskStep = UnityEngine.Object.Instantiate<GameObject>(task_step_prefab, parent_transform)
            .GetComponent<TaskStep>();


            // Continue from here 
             
            //  taskStep.Initialize_Quest_Step(info.id);
        }
    }

    private GameObject Get_Current_Task_Step_Prefab()
    {
        GameObject task_step_prefab = null;

        if (Current_Step_Exists())
        {
            task_step_prefab = info.task_step_prefabs[current_task_step_index];
        }
        else
        {
            Debug.LogWarning("Task step index was out of range");
        }

        return task_step_prefab;
    }
}