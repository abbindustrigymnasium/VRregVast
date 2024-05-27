using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TaskStepState
{
    public string state;

    public TaskStepState(string state)
    {
        this.state = state;
    }

    public TaskStepState()
    {
        this.state = "";
    }
}