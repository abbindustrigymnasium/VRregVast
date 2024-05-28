/*
 * This script should be added to each tool that should be able to pick up objects
 *
 * This script is like XRDirectInteractor for hands, but is ment for tools
 *
 * Written by Hampus Fridholm
 *
 * 2024-05-24
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ToolInteractorEvent : UnityEvent<GameObject>
{
  
}

[RequireComponent(typeof(ToolActivation))]
public class ToolInteractor : MonoBehaviour
{
  // The position and rotation of the attachment point for picked up objects
  [SerializeField]
  public Transform collision_transform;

  // The range from the attachment point to be able to pick up objects
  [SerializeField]
  private float collision_radius = 1;

  // The specific layer to interact with
  [SerializeField]
  private LayerMask collision_layer;

  // The activation threshold for picking up objects
  [SerializeField]
  private float select_activation = 0.5f;

  public GameObject select_object;
  public GameObject target_object;

  public ToolInteractorEvent selectEntered = new ToolInteractorEvent();
  public ToolInteractorEvent selectExited  = new ToolInteractorEvent();

  // select_ability is a resetable check if an object can be picked up
  private bool select_ability = false;

  private ToolActivation tool_activation;

  /*
   * On initialization, get the tool activation and rigidbody components
   */
  void Awake()
  {
    tool_activation = GetComponent<ToolActivation>();

    selectEntered.AddListener(On_Select_Entered);

    selectExited.AddListener(On_Select_Exited);
  }

  /*
   *
   */
  void onDestroy()
  {
    selectEntered.RemoveListener(On_Select_Entered);

    selectExited.RemoveListener(On_Select_Exited);
  }

  /*
   *
   */
  private void On_Select_Entered(GameObject new_object)
  {
    select_object = new_object;
  }

  /*
   *
   */
  private void On_Select_Exited(GameObject old_object)
  {
    select_object = null;
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
      GameObject current_object = collider.gameObject;

      // If the object does not have the component ToolInteractable,
      // it can not be picked up
      if(!current_object.GetComponent<ToolInteractable>()) continue;


      Vector3 current_position = current_object.transform.position;

      float current_distance = Vector3.Distance(current_position, collision_transform.position);

      if(closest_distance == -1 || current_distance < closest_distance)
      {
        closest_object   = current_object;
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
      if(select_ability && !select_object)
      {
        closest_object?.GetComponent<ToolInteractable>()?.selectEntered.Invoke(this.gameObject);

        select_ability = false;
      }
    }
    else
    {
      select_object?.GetComponent<ToolInteractable>()?.selectExited.Invoke(this.gameObject);

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

    target_object = (select_object) ? null : closest_object;
  }
}
