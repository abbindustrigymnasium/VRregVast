using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private void Change_Task_State(string id, TaskState state)
    {
        Task task = Get_Task_By_Id(id);
        task.state = state;
        EventsManager.instance.task_events.Task_State_Change(task);
    }

    private bool Check_Requirements_Met(Task task)
    {
        bool meets_requirements = true;

        foreach (TaskInfoSO prereq_info in task.info.prerequisites)
        {
            if (Get_Task_By_Id(prereq_info.id).state != TaskState.FINISHED)
            {
                meets_requirements = false;
            }
        }

        return meets_requirements;
    }

    private void Update()
    {
        foreach (Task task in task_map.Values)
        {
            if (task.state == TaskState.REQUIREMENTS_NOT_MET && Check_Requirements_Met(task))
            {
                Change_Task_State(task.info.id, TaskState.CAN_START);
            }
        }
    }

    private void Start_Task(string id)
    {
        Task task = Get_Task_By_Id(id);
        task.Instantiate_Current_Task_Step(this.transform);
        Change_Task_State(task.info.id, TaskState.IN_PROGRESS);

        Debug.Log("Task " + id + " has been started!");
    }

    private void Advance_Task(string id)
    {
        Task task = Get_Task_By_Id(id);
        task.Move_To_Next_Step();

        if (task.Current_Step_Exists())
        {
            task.Instantiate_Current_Task_Step(this.transform);
        }
        else
        {
            Change_Task_State(task.info.id, TaskState.CAN_FINISH);
        }

        Debug.Log("Task " + id + " has been advanced!");

    }

    private void Finish_Task(string id)
    {
        Task task = Get_Task_By_Id(id);
        Claim_Rewards(task);

        Change_Task_State(task.info.id, TaskState.FINISHED);

        Debug.Log("Task " + id + " has been finished!");

    }

    private void Claim_Rewards(Task task)
    {
        // Connect with lvling system
        Debug.Log("Player recieved " + task.info.exp_gain + " exp! (In theory)");
    }

    private void Task_Step_State_Change(string id, int stepIndex, TaskStepState task_step_state)
    {
        Task task = Get_Task_By_Id(id);
        Change_Task_State(id, task.state);
    }

    private Dictionary<string, Task> Create_Task_Map()
    {
        TaskInfoSO[] all_tasks = Resources.LoadAll<TaskInfoSO>("Tasks/" + SceneManager.GetActiveScene().name);

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
            Debug.LogError(id + " not found in task map");
        }

        return task;
    }
}