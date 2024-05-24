/*
 * This script should be added to each object that should be able to be picked up by tools
 *
 * This script is like XRGrabInteractable for hands, but is ment for tools
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
public class ToolInteractableEvent : UnityEvent<GameObject>
{
  
}

public class ToolInteractable : MonoBehaviour
{
  public ToolInteractableEvent selectEntered = new ToolInteractableEvent();
  public ToolInteractableEvent selectExited  = new ToolInteractableEvent();

  private GameObject tool_object;

  private Rigidbody this_rigidbody;

  void Awake()
  {
    this_rigidbody = GetComponent<Rigidbody>();

    selectEntered.AddListener(On_Select_Entered);

    selectExited.AddListener(On_Select_Exited);
  }

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
    // 1. Call the new tool's selectEntered event
    new_object.GetComponent<ToolInteractor>()?.selectEntered.Invoke(this.gameObject);

    // 2. Call the old tool's selectExited event
    tool_object?.GetComponent<ToolInteractor>()?.selectExited.Invoke(this.gameObject);

    // 3. Update the tool to be the new tool
    tool_object = new_object;
  }

  /*
   *
   */
  private void On_Select_Exited(GameObject old_object)
  {
    // 1. Call the old tool's selectExited event
    tool_object?.GetComponent<ToolInteractor>()?.selectExited.Invoke(this.gameObject);

    // 2. Set the tool to be null
    tool_object = null;
  }

  /*
   * Each frame, update this objects position and rotation
   * to reflect that of the tool's that is holding this object
   */
  void Update()
  {
    ToolInteractor interactor = tool_object?.GetComponent<ToolInteractor>();

    if(interactor)
    {
      transform.position = interactor.collision_transform.position;

      transform.rotation = interactor.collision_transform.rotation;
    }

    Rigidbody tool_rigidbody = tool_object?.GetComponent<Rigidbody>();

    if(this_rigidbody && tool_rigidbody)
    {
      this_rigidbody.velocity = tool_rigidbody.velocity;
    }
  }
}
