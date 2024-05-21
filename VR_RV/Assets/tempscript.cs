using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempscript : MonoBehaviour
{
    private void OnTriggerEnter()
    {
        GameObject.Find("MoveBlockTaskStepOne").GetComponent<MoveBlockTaskStepOne>().Block_Picked_Up();
    }
}
