using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSystem : MonoBehaviour
{
    public float current_time;
    public bool pause_timer;

    void Update()
    {
        if (pause_timer)
        {
            current_time += Time.deltaTime;
        }
    }
}
