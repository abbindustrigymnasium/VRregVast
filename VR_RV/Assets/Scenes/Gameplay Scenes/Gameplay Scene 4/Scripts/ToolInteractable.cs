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
  [SerializeField]
  private float max_linear_velocity = 5f;
  
  [SerializeField]
  private float max_angular_velocity = 5f;

  public ToolInteractableEvent selectEntered = new ToolInteractableEvent();
  public ToolInteractableEvent selectExited  = new ToolInteractableEvent();

  private GameObject tool_object;

  private Rigidbody this_rigidbody;

  private Vector3 last_position;
  private Vector3 last_euler_angles;

  private Vector3 linear_velocity;
  private Vector3 angular_velocity;

  void Awake()
  {
    this_rigidbody = GetComponent<Rigidbody>();

    selectEntered.AddListener(On_Select_Entered);

    selectExited.AddListener(On_Select_Exited);
  }

  void Start()
  {
    last_position = transform.position;

    last_euler_angles = transform.eulerAngles;
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

    //
    this_rigidbody.useGravity = false;

    if(this_rigidbody)
    {
      this_rigidbody.velocity = Vector3.zero;
      
      this_rigidbody.angularVelocity = Vector3.zero;
    }

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

    // 2. Give object the same exiting velocity as the tool attach point
    this_rigidbody.useGravity = true;

    if(this_rigidbody)
    {
      this_rigidbody.velocity = Vector3.ClampMagnitude(linear_velocity, max_linear_velocity);
      
      this_rigidbody.angularVelocity = Vector3.ClampMagnitude(angular_velocity, max_angular_velocity);
    }

    // 3. Set the tool to be null
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
  }

  void FixedUpdate()
  {
    linear_velocity = (transform.position - last_position) / Time.fixedDeltaTime;

    angular_velocity = (transform.eulerAngles - last_euler_angles) / Time.fixedDeltaTime;


    last_position = transform.position;

    last_euler_angles = transform.eulerAngles;
  }
}
