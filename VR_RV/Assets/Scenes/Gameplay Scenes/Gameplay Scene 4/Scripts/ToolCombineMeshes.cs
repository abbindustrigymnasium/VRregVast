/*
 * Copy meshes from children into the parent's Mesh.
 * CombineInstance stores the list of meshes.  These are combined
 * and assigned to the attached Mesh.
 *
 * Written by Hampus Fridholm
 *
 * 2024-05-28
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(ToolActivation))]
public class ToolCombineMeshes : MonoBehaviour
{
  private ToolActivation tool_activation;

  private MeshFilter mesh_filter;

  private MeshRenderer mesh_renderer;
  
  private MeshCollider mesh_collider;

  private float last_activation;

  void Start()
  {
    tool_activation = GetComponent<ToolActivation>();

    mesh_filter = GetComponent<MeshFilter>();

    mesh_renderer = GetComponent<MeshRenderer>();

    mesh_collider = GetComponent<MeshCollider>();

    StartCoroutine(ExampleCoroutine());
  }

  IEnumerator ExampleCoroutine()
  {
    yield return new WaitForSeconds(1);
    
    UpdateMesh();
  }

  public void UpdateMesh()
  {
    List<MeshFilter> mesh_filters = new List<MeshFilter>();

    mesh_filters.AddRange(tool_activation.main_object.GetComponentsInChildren<MeshFilter>());

    mesh_filters.AddRange(tool_activation.move_object.GetComponentsInChildren<MeshFilter>());

    
    List<CombineInstance> combine_list = new List<CombineInstance>();

    List<Material> material_list = new List<Material>();


    for(int index = 0; index < mesh_filters.Count; index++)
    {
      if(ReferenceEquals(mesh_filters[index], mesh_filter)) continue;


      MeshFilter curr_filter = mesh_filters[index];

      Mesh curr_mesh = curr_filter.sharedMesh;
      
      if(!curr_mesh || !curr_mesh.isReadable) continue;


      CombineInstance curr_combine = new CombineInstance();

      curr_combine.mesh = curr_mesh;

      curr_combine.transform = transform.worldToLocalMatrix * curr_filter.transform.localToWorldMatrix;

      combine_list.Add(curr_combine);


      MeshRenderer curr_renderer = curr_filter.gameObject.GetComponent<MeshRenderer>();

      material_list.AddRange(curr_renderer.materials);

      curr_renderer.enabled = false;
    }


    if(combine_list.Count > 0)
    {
      Mesh mesh = new Mesh();

      mesh.CombineMeshes(combine_list.ToArray());

      mesh_filter.sharedMesh = mesh;

      mesh_renderer.materials = material_list.ToArray();

      if(mesh_collider)
      {
        mesh_collider.convex = true;

        mesh_collider.sharedMesh = mesh;
      }
    }

    gameObject.SetActive(true);
  }

  void Update()
  {
    if(tool_activation.activation != last_activation)
    {
      UpdateMesh();

      last_activation = tool_activation.activation;
    }
  }
}
