// using System;

// public class TaskEvents
// {
//     public event Action<string> On_Start_Task;
//     public void Start_Task(string id)
//     {
//         if (On_Start_Task != null)
//         {
//             On_Start_Task(id);
//         }
//     }

//     public event Action<string> On_Advance_Task;
//     public void Advance_Task(string id)
//     {
//         if (On_Advance_Task != null)
//         {
//             On_Advance_Task(id);
//         }
//     }

//     public event Action<string> On_Finish_Task;
//     public void Finish_Task(string id)
//     {
//         if (On_Finish_Task != null)
//         {
//             On_Finish_Task(id);
//         }
//     }

//     public event Action<Task> On_Task_State_Change;
//     public void Task_State_Change(Task task)
//     {
//         if (On_Task_State_Change != null)
//         {
//             On_Task_State_Change(task);
//         }
//     }

//     public event Action<string, int, TaskStepState> On_Task_Step_State_Change;
//     public void Task_Step_State_Change(string id, int step_index, TaskStepState task_step_state)
//     {
//         if (On_Task_Step_State_Change != null)
//         {
//             On_Task_Step_State_Change(id, step_index, task_step_state);
//         }
//     }
// }