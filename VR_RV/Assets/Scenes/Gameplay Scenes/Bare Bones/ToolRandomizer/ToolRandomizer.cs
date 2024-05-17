using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolRandomizer : MonoBehaviour
{
    public GameObject[] all_tools;


    public List<Vector3> tool_spawnpoints_list = new List<Vector3>();

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

        tool.transform.position = tool_spawnpoints_list[index];

        tool_spawnpoints_list.Remove(tool_spawnpoints_list[index]);
      }

    }
}
