using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlockTaskStepOne : TaskStep
{

    // No applicable event is currently implemented, 
    // so a public method is used to determine that the block has been picked up 
    // (and thus the task step is finished)
    public void Block_Picked_Up()
    {
        Finish_Step();
    }
}
