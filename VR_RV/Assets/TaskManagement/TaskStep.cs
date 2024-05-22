using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class TaskStep : MonoBehaviour
{
    private bool is_finished = false;

    private string quest_id;

    public void Initialize_Quest_Id(string quest_id)
    {
        this.quest_id = quest_id;
    }

    protected void Finish_Step()
    {
        if (!is_finished)
        {
            is_finished = true;
            EventsManager.instance.task_events.Advance_Task(quest_id);
            Destroy(this.gameObject);
        }

    }
}