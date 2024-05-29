using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishedGivingObjectsScript : TaskStep
{
    public void Done()
    {
        Transform headTransform = GameObject.Find("Room/Patient/Head")?.transform;

        if (headTransform != null)
        {
            headTransform.gameObject.SetActive(true);
            Debug.Log("Head GameObject has been activated.");
        }
        else
        {
            Debug.LogError("Head GameObject not found.");
        }

        Finish_Step();
    }

    protected override void Set_Task_Step_State(string state)
    {
        // none in this case
    }
}
