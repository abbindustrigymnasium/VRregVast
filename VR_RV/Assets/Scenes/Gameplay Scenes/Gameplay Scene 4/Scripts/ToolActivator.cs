/*
 * This script should be added to the hands that will pick up and use tools
 *
 * This script links the trigger on a controller to the activation of a tool
 *
 * Written by Hampus Fridholm
 *
 * 2024-05-24
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class ToolActivator : MonoBehaviour
{
  // Action Value from hand trigger reference
  [SerializeField]
  private InputActionReference trigger_reference;

  private XRDirectInteractor direct_interactor;

  private GameObject selected_tool;

  /*
   * Before the script is loaded, initialize event listeners
   */
  void Awake()
  {
    direct_interactor = GetComponent<XRDirectInteractor>();

    if(direct_interactor)
    {
      direct_interactor.selectEntered.AddListener(On_Select_Enter);

      direct_interactor.selectExited.AddListener(On_Select_Exit);
    }
  }

  /*
   * When the script is unloaded, remove the event listeners
   */
  void onDestroy()
  {
    if(direct_interactor)
    {
      direct_interactor.selectEntered.RemoveListener(On_Select_Enter);

      direct_interactor.selectExited.RemoveListener(On_Select_Exit);
    }
  }

  /*
   * When the user picks up an object, store the object if it is a tool
   */
  private void On_Select_Enter(SelectEnterEventArgs args)
  {
    GameObject selected_object = args.interactableObject.transform.gameObject;

    if(selected_object?.GetComponent<ToolActivation>())
    {
      selected_tool = selected_object;
    }
  }

  /*
   * When the user drops a tool, inactivate the tool
   */
  private void On_Select_Exit(SelectExitEventArgs args)
  {
    ToolActivation tool_activation = selected_tool?.GetComponent<ToolActivation>();

    if(tool_activation) tool_activation.activation = 0.0f;

    selected_tool = null;
  }

  /*
   * Each frame, if the user is holding a tool,
   * update the tools activaiton based on how much the trigger is pushed in
   */
  void Update()
  {
    if(selected_tool && trigger_reference)
    {
      float trigger_value = trigger_reference.action.ReadValue<float>();

      ToolActivation tool_activation = selected_tool?.GetComponent<ToolActivation>();

      if(tool_activation) tool_activation.activation = trigger_value;
    }
  }
}
