/*
 * This script should be added to an empty object acting as the spawn point for the tools
 *
 * Written by Ted Strömne
 *
 * 2024-05-17
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolSpawnScript : MonoBehaviour
{
    // Create a list of GameObjects
    public List<GameObject> spawned_tools;
    void Start()
    {
        // Loop through the GameObjects and spawn each one
        foreach (GameObject tool in spawned_tools) {
            GameObject newObject = Instantiate(tool, transform.position, transform.rotation);
        }
    }
}
