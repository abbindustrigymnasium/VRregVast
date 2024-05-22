/*
 * This script should be added to each object that will be interacted by the hands
 *
 * Written by Hampus Fridholm
 *
 * 2024-04-23
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OutlineReceiver : MonoBehaviour
{
  private GameObject outline_object;

  /*
   * Create an outline object for the hands to add outlines to
   */
  void Awake()
  {
    // Create a new GameObject to display the outline material
    outline_object = new GameObject("Outline");
    outline_object.transform.SetParent(transform, false);

    // Copy the MeshFilter from the original object
    MeshFilter mesh_filter = outline_object.AddComponent<MeshFilter>();
    mesh_filter.sharedMesh = (GetComponent<MeshFilter>()) ? GetComponent<MeshFilter>().sharedMesh : GetComponentInChildren<MeshFilter>().sharedMesh;

    // Add a MeshRenderer and apply the outline material
    MeshRenderer outline_renderer = outline_object.AddComponent<MeshRenderer>();
    outline_renderer.material = null;

    // Initially disable the outline object
    outline_object.SetActive(false);
  }

  /*
   * Destroy the created outline object
   */
  void onDestroy()
  {
    // Add code here if you want :D
  }
}
