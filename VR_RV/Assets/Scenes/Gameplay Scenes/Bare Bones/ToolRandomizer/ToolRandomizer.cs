using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolRandomizer : MonoBehaviour
{
    [SerializeField] private GameObject[] all_tools;

    [SerializeField] private List<Vector3> tool_spawnpoints_list = new List<Vector3>();

    private Vector3[] saved_spawnpoints;

    private float timer = 0f;

    void Awake(){
      saved_spawnpoints = tool_spawnpoints_list.ToArray();
    }

    void Update(){
      if(timer >= 5.0f){
        Randomize_Items("Tool");
        timer = 0f;
        tool_spawnpoints_list = saved_spawnpoints.ToList();
      }
      else{
        timer += Time.deltaTime;
      }
    }

    // Randomize spawn positions of items with specific tag
    void Randomize_Items(string tag){
      all_tools = GameObject.FindGameObjectsWithTag(tag);

      foreach(GameObject tool in all_tools){
        int index = Random.Range(0, tool_spawnpoints_list.Count);

        // Set tool position to random position from list
        tool.transform.position = tool_spawnpoints_list[index];

        // Remove position from list to avoid tools spawning on the same point
        tool_spawnpoints_list.Remove(tool_spawnpoints_list[index]);
      }

    }
}
