using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolRandomizer : MonoBehaviour
{
    public GameObject[] all_tools;

    public Vector3[] tool_spawnpoints;

    // Start is called before the first frame update
    void Start()
    {
      all_tools = GameObject.FindGameObjectsWithTag("Tool");

      foreach(GameObject tool in all_tools){
        tool.AddComponent<Rigidbody>();
        tool.transform.position = tool_spawnpoints[Random.Range(0, tool_spawnpoints.Length)];
      }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
