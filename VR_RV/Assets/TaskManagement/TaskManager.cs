using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class TaskManager : MonoBehaviour
{
    private Dictionary<string, Task> task_map;
    private int total_tasks = 0;
    private TMP_Text task_display;

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
        task_display = GameObject.FindWithTag("Task Display").GetComponent<TMP_Text>();

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
                Start_Task(task.info.id);
            }

            if (task.state == TaskState.CAN_FINISH)
            {
                Change_Task_State(task.info.id, TaskState.FINISHED);
                Finish_Task(task.info.id);
            }
        }
    }

    private void Start_Task(string id)
    {
        Task task = Get_Task_By_Id(id);
        task.Instantiate_Current_Task_Step(this.transform);
        Change_Task_State(task.info.id, TaskState.IN_PROGRESS);

        Display(task.info);
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

        Display(task.info);
    }

    private void Finish_Task(string id)
    {
        Task task = Get_Task_By_Id(id);
        Claim_Rewards(task);

        Change_Task_State(task.info.id, TaskState.FINISHED);

        total_tasks -= 1;

        if (total_tasks == 0)
        {
            StartCoroutine(EndScene());
            Display(null, true, true);
        }
        else
        {
            Display(null, true);
        }
    }

    IEnumerator EndScene()
    {
        yield return new WaitForSeconds(5);

        Debug.Log("Switching scene! (in theory...)");
        VRregVast.StandardLibrary.SceneManagement.New_Scene();
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

        total_tasks = all_tasks.Length;

        Dictionary<string, Task> id_to_task_map = new Dictionary<string, Task>();

        foreach (TaskInfoSO task_info in all_tasks)
        {
            Debug.Log("Task: " + task_info.display_name);
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

    private void Display(TaskInfoSO info, bool completed = false, bool all_completed = false)
    {
        if (completed)
        {
            task_display.text = "Alla uppgifter i denna scen är färdiga!\nDu skickas till nästa scen om 10 sekunder";
        }
        else
        {
            if (completed)
            {
                task_display.text = "";
            }
            else
            {
                task_display.text = "Uppgift:\n" + info.display_name + "\nSteg:\n" + info.task_step_prefabs[0].name;

            }
        }
    }
}