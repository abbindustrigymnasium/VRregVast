using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolRandomizer : MonoBehaviour
{
    [SerializeField] private GameObject[] all_tools;

    [SerializeField] private List<Vector3> tool_spawnpoints_list = new List<Vector3>();

    void Start()
    {
      Randomize_Items("Tool");
    }

    // Randomize spawn positions of items with tag
    void Randomize_Items(string tag){
      all_tools = GameObject.FindGameObjectsWithTag(tag);

      foreach(GameObject tool in all_tools){
        int index = Random.Range(0, tool_spawnpoints_list.Count);

        //Temporary
        tool.AddComponent<Rigidbody>();

        // Set position to random position from list
        tool.transform.position = tool_spawnpoints_list[index];

        // Remove position from list to avoid spawning on the same point
        tool_spawnpoints_list.Remove(tool_spawnpoints_list[index]);
      }

    }
}
