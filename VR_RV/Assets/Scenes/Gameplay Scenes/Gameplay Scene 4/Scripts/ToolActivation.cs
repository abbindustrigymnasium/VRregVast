/*
 * This script should be added to each tool that can be activated
 *
 * Note: This script will be replaced by Noah's script with actual animations
 *
 * Written by Hampus Fridholm
 *
 * 2024-05-22
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolActivation : MonoBehaviour
{
  public float activation;

  private MeshRenderer mesh_renderer;

  void Awake()
  {
    mesh_renderer = GetComponent<MeshRenderer>();
  }

  void Update()
  {
    if(mesh_renderer?.material)
    {
      mesh_renderer.material.color = Color.Lerp(Color.green, Color.red, activation);
    }
  }
}
