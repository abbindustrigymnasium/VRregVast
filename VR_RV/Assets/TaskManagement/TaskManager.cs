using System;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    private Dictionary<string, Task> task_map;

    private void Awake()
    {
        task_map = Create_Task_Map();

        Task task = Get_Task_By_Id("MoveBlock");

        Debug.Log(task.info.display_name);
        Debug.Log(task.state);
        Debug.Log(task.Current_Step_Exists());
    }

    private Dictionary<string, Task> Create_Task_Map()
    {
        TaskInfoSO[] all_tasks = Resources.LoadAll<TaskInfoSO>("Tasks");

        Dictionary<string, Task> id_to_task_map = new Dictionary<string, Task>();

        foreach (TaskInfoSO task_info in all_tasks)
        {
            if (id_to_task_map.ContainsKey(task_info.id))
            {
                Debug.LogWarning("Duplicate id found when creating task map");
            }
            id_to_task_map.Add(task_info.id, new Task(task_info));
        }

        return id_to_task_map;
    }

    private Task Get_Task_By_Id(string id)
    {
        Task task = task_map[id];

        if (task == null)
        {
            Debug.LogError("Id not found in task map");
        }

        return task;
    }
}