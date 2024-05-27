using System;
using System.Collections.Generic;
using UnityEngine;

public enum TaskState
{
    REQUIREMENTS_NOT_MET,
    CAN_START,
    IN_PROGRESS,
    CAN_FINISH,
    FINISHED
}