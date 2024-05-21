// using System;
// using UnityEngine;

// public class EventsManager : MonoBehaviour
// {
//     public static EventsManager instance { get; private set; }
//     public TaskEvents task_events;

//     private void Awake()
//     {
//         if (instance != null)
//         {
//             Debug.LogError("Found more than one Events Manager in the scene.");
//         }
//         instance = this;

//         // Initialize events
//         task_events = new TaskEvents();
//     }
// }
