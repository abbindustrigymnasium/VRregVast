using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script was writen by Simon Meier

//It's use is to count

public class CountBlood : MonoBehaviour
{
    public int bloodCount = 0;

    public int maxCount = 10;

    // Update is called once per frame
    void Update()
    {
        if (bloodCount > maxCount)
        {
            Transform headTransform = GameObject.Find("Room/Patient/Head")?.transform;

            if (headTransform != null)
            {
                headTransform.gameObject.SetActive(false);
            }
            else
            {
                Debug.LogError("Head GameObject not found.");
            }
        }
    }
    public void AddScore()
    {
        bloodCount += 1;
    }
}
