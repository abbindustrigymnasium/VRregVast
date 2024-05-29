/*
 * This script should be added to each object that will be interacted by the hands
 *
 * Written by Hampus Fridholm
 *
 * 2024-05-28
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(MeshFilter))]
public class OutlineReceiver : MonoBehaviour
{
  private GameObject outline_object;

  private MeshFilter mesh_filter;

  /*
   * Create an outline object for the hands to add outlines to
   */
  void Awake()
  {
    // Create a new GameObject to display the outline material
    outline_object = new GameObject("Outline");
    outline_object.transform.SetParent(transform, false);

    // Copy the MeshFilter from the original object
    mesh_filter = outline_object.AddComponent<MeshFilter>();

    // Add a MeshRenderer and apply the outline material
    MeshRenderer outline_renderer = outline_object.AddComponent<MeshRenderer>();
    outline_renderer.material = null;

    // Initially disable the outline object
    outline_object.SetActive(false);
  }

  void Update()
  {
    mesh_filter.sharedMesh = GetComponent<MeshFilter>().sharedMesh;
  }
}
