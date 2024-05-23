/*
 * This script should be added to each tool that should be able to pick up objects
 *
 * Written by Hampus Fridholm
 *
 * 2024-05-22
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolInteractor : MonoBehaviour
{
  [SerializeField] private Transform collision_transform;

  [SerializeField] private float     collision_radius = 1;
  [SerializeField] private LayerMask collision_layer;

  [SerializeField] private float select_activation = 0.9f;

  public GameObject select_object;
  public GameObject target_object;

  private bool select_ability = false;

  private ToolActivation tool_activation;

  private Rigidbody tool_rigidbody;

  void Awake()
  {
    tool_activation = GetComponent<ToolActivation>();

    tool_rigidbody  = GetComponent<Rigidbody>();
  }

  /*
   * Get the closest object in reach of the tool
   *
   * RETURN (GameObject closest_object)
   * - null | If no object is in reach of the tool
   */
  private GameObject Get_Closest_Object()
  {
    Collider[] colliders = Physics.OverlapSphere(collision_transform.position, collision_radius, collision_layer);

    GameObject closest_object   = null;
    float      closest_distance = -1;
    
    foreach(Collider collider in colliders)
    {
      Vector3 current_position = collider.gameObject.transform.position;

      float current_distance = Vector3.Distance(current_position, collision_transform.position);

      if(closest_distance == -1 || current_distance < closest_distance)
      {
        closest_object   = collider.gameObject;
        closest_distance = current_distance;
      }
    }

    return closest_object;
  }

  /*
   * Update the object the tool is holding,
   * either by assigning the closest object if the trigger is pressed in,
   * or by continuing holding the current selected object
   *
   * PARAMS
   * - GameObject closest_object | The closest object colliding with tool
   */
  private void Update_Selected_Object(GameObject closest_object)
  {
    if(tool_activation.activation >= select_activation)
    {
      if(select_ability && select_object == null)
      {
        select_object = closest_object;

        select_ability = false;
      }
    }
    else
    {
      select_object = null;

      select_ability = true;
    }
  }

  /*
   * Each frame, get the closest reachable object,
   * update which object the tool is holding,
   * update the object's position to match the tool's,
   */
  void Update()
  {
    GameObject closest_object = Get_Closest_Object();

    Update_Selected_Object(closest_object);

    if(select_object)
    {
      target_object = null;

      select_object.transform.position = collision_transform.position;

      select_object.transform.rotation = collision_transform.rotation;

      Rigidbody rigidbody = select_object?.GetComponent<Rigidbody>();

      if(rigidbody && tool_rigidbody)
      {
        // rigidbody.velocity = tool_rigidbody.velocity;
      }

    }
    else target_object = closest_object;
  }
}
