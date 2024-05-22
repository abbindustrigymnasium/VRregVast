using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TaskPoint : MonoBehaviour
{
    [Header("Task")]
    [SerializeField] private TaskInfoSO task_info_for_point;

    private string task_id;
    private TaskState current_task_state;

    private void Awake()
    {
        task_id = task_info_for_point.id;
    }

    private void OnEnable()
    {
        EventsManager.instance.task_events.on_task_state_change += TaskStateChange;
    }

    private void onDisable()
    {
        EventsManager.instance.task_events.on_task_state_change -= TaskStateChange;
    }

    private void Met()
    {
        EventsManager.instance.task_events.Start_Task(task_id);
        EventsManager.instance.task_events.Advance_Task(task_id);
        EventsManager.instance.task_events.Finish_Task(task_id);
    }

    private void TaskStateChange(Task task)
    {
        if (task.info.id.Equals(task_id))
        {
            current_task_state = task.state;
            Debug.Log("Task with id: " + "updated to state: " + current_task_state);
        }
    }

    private void onTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            Debug.Log("Hand felt");
            Met();
        }
    }
}