using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script was writen by Simon Meier

//It's use is to count

public class CountBlood : MonoBehaviour
{
    public int bloodCount = 0;

    public int maxCount = 10;

    public void AddScore()
    {
        bloodCount += 1;

        if (bloodCount > maxCount)
        {
            GameObject.Find("Task Manager").GetComponentInChildren<VacuumBloodStep>().Done();
        }
    }
}
