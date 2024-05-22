using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlockTaskStepMoveBlock : TaskStep
{
    // No applicable event is currently implemented, 
    // so a public method is used to determine that the step is completed
    public void Trigger()
    {
        Finish_Step();
    }

    protected override void Set_Task_Step_State(string state)
    {
        // none in this case
    }
}
