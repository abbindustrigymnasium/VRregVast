using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortItemsStep : TaskStep
{
    public void All_Sorted()
    {
        Finish_Step();
    }

    protected override void Set_Task_Step_State(string state)
    {
        // none in this case
    }
}
