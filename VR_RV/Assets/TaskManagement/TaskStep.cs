using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class TaskStep : MonoBehaviour
{
    private bool is_finished = false;

    protected void Finish_Step()
    {
        if (!is_finished)
        {
            is_finished = true;
        }

        Destroy(this.gameObject);
    }
}