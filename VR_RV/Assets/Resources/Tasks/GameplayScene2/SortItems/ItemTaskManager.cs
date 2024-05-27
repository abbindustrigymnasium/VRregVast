using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTaskManager : MonoBehaviour
{
    [SerializeField] private int items;
    [SerializeField] private int correct_items;

    public void Add_Item()
    {
        items += 1;
    }

    public void Correct()
    {
        correct_items += 1;

        if (correct_items == items)
        {
            GameObject.Find("Task Manager").GetComponentInChildren<SortItemsStep>().All_Sorted();
        }
    }
}