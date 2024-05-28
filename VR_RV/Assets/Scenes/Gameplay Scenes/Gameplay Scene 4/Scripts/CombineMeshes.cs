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
[RequireComponent(typeof(MeshCollider))]
public class CombineMeshes : MonoBehaviour
{
  void UpdateMesh()
  {
    MeshFilter[] mesh_filters = GetComponentsInChildren<MeshFilter>();

    List<CombineInstance> combine_list = new List<CombineInstance>();


    for(int index = 0; index < mesh_filters.Length; index++)
    {
      MeshFilter curr_filter = mesh_filters[index];

      Mesh curr_mesh = curr_filter.sharedMesh;
      
      if(!curr_mesh || !curr_mesh.isReadable) continue;


      CombineInstance curr_combine = new CombineInstance();

      curr_combine.mesh = curr_mesh;

      curr_combine.transform = transform.worldToLocalMatrix * curr_filter.transform.localToWorldMatrix;

      combine_list.Add(curr_combine);


      mesh_filters[index].gameObject.SetActive(false);
    }


    if(combine_list.Count > 0)
    {
      Mesh mesh = new Mesh();

      mesh.CombineMeshes(combine_list.ToArray());

      transform.GetComponent<MeshFilter>().sharedMesh = mesh;


      transform.GetComponent<MeshCollider>().convex = true;

      transform.GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    transform.gameObject.SetActive(true);
  }

  void Update()
  {
    UpdateMesh();
  }
}
