using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOffPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "BLOCK")
        {
            GameObject.Find("Task Manager").GetComponentInChildren<MoveBlockTaskStepMoveBlock>().Trigger();
        }
    }
}
