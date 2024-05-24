using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TaskInfo", menuName = "Tasks/TaskInfoSO")]
public class TaskInfoSO : ScriptableObject
{
    [field: SerializeField] public string id { get; private set; }

    [Header("General")]
    public string display_name;

    [Header("Requirements")]
    public TaskInfoSO[] prerequisites;

    [Header("Steps")]
    public GameObject[] task_step_prefabs;

    [Header("Experience Reward")]
    public int exp_gain;

    // Forces id to be = file name
    private void OnValidate()
    {
#if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}
