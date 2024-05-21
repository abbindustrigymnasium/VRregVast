using System;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    private Dictionary<string, Task> task_map;

    private void Awake()
    {
        task_map = Create_Task_Map();
    }
    private void OnEnable()
    {
        EventsManager.instance.task_events.on_start_task += Start_Task;
        EventsManager.instance.task_events.on_advance_task += Advance_Task;
        EventsManager.instance.task_events.on_finish_task += Finish_Task;
    }

    private void OnDisable()
    {
        EventsManager.instance.task_events.on_start_task -= Start_Task;
        EventsManager.instance.task_events.on_advance_task -= Advance_Task;
        EventsManager.instance.task_events.on_finish_task -= Finish_Task;
    }

    private void Start()
    {
        foreach (var task in task_map.Values)
        {
            EventsManager.instance.task_events.Task_State_Change(task);
        }
    }

    private void Start_Task(string id)
    {

    }

    private void Advance_Task(string id)
    {

    }

    private void Finish_Task(string id)
    {

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