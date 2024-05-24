/*
 * This script should be added to each tool that should be able to pick up objects
 *
 * This script is like OutlineCreator for hands, but is ment for tools
 *
 * Written by Hampus Fridholm
 *
 * 2024-05-24
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

public class ToolOutlineCreator : MonoBehaviour
{
   // This is the type of outline you want to give all nerby objects
  [SerializeField] private Material outline_material;

  private ToolInteractor     tool_interactor;
  private XRGrabInteractable grab_interactable;

  private GameObject last_outline_object;

  private bool tool_selected = false;

  /*
   * Get the ToolInteractor component for accessing target object
   */
  void Awake()
  {
    tool_interactor = GetComponent<ToolInteractor>();

    grab_interactable = GetComponent<XRGrabInteractable>();

    if(grab_interactable)
    {
      grab_interactable.selectEntered.AddListener(On_Select_Enter);

      grab_interactable.selectExited.AddListener(On_Select_Exit);
    }
  }

  /*
   * When the script is unloaded, remove the event listeners
   */
  void onDestroy()
  {
    if(grab_interactable)
    {
      grab_interactable.selectEntered.RemoveListener(On_Select_Enter);

      grab_interactable.selectExited.RemoveListener(On_Select_Exit);
    }
  }

  /*
   * When the user picks up the tool, set tool_selected to true
   */
  private void On_Select_Enter(SelectEnterEventArgs args)
  {
    tool_selected = true;
  }

  /*
   * When the user drops the tool, set tool_selected to false
   */
  private void On_Select_Exit(SelectExitEventArgs args)
  {
    tool_selected = false;
  }

  /*
   * Add outline material to outline object mesh renderer
   *
   * PARAMS
   * - GameObject outline_object | The outline object to add the material to
   */
  private void Add_Outline_Material(GameObject outline_object)
  {
    MeshRenderer outline_renderer = outline_object?.GetComponent<MeshRenderer>();

    if(outline_renderer) outline_renderer.material = outline_material;
  }

  /*
   * Get the outline object for the object that the tool is targeting
   *
   * RETURN (GameObject outline_object)
   * - null | If the tool is not targeting any object
   */
  private GameObject Get_Target_Outline_Object()
  {
    GameObject target_object = tool_interactor?.target_object;

    return target_object?.transform.Find("Outline")?.gameObject;
  }

  /*
   * Continuously add the outline material to the object that the tool is targeting
   * If the tool has selected an object, no object outline should be active
   */
  void Update()
  {
    // If the tool is already holding an object, or the tool is dropped
    // no object should be outlined
    if(tool_interactor.select_object || !tool_selected)
    {
      last_outline_object?.SetActive(false);

      last_outline_object = null;
    }
    else // Outline just the object that the tool is targeting
    {
      GameObject outline_object = Get_Target_Outline_Object();

      Add_Outline_Material(outline_object);

      last_outline_object?.SetActive(false);

      outline_object?.SetActive(true);

      last_outline_object = outline_object;
    }
  }
}
