using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockHelper : MonoBehaviour
{
    public void PickedUp()
    {
        GameObject step_go = GameObject.Find("MoveBlockTaskStepPickup(clone)");
        if (step_go != null)
        {
            step_go.GetComponent<MoveBlockTaskStepPickup>().Trigger();
        }
    }

    public void Dropped()
    {
        GameObject step_go = GameObject.Find("MoveBlockTaskStepDrop(clone)");
        if (step_go != null)
        {
            step_go.GetComponent<MoveBlockTaskStepDrop>().Trigger();
        }
    }
}
