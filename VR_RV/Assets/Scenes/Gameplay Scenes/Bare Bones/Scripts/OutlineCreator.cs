/*
        //Temporary
        if(execution_number == 0)
            tool.AddComponent<Rigidbody>();
 * This script should be added to each hand that is going to interact with objects
 *
 * Written by Hampus Fridholm
 *
 * 2024-04-24
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OutlineCreator : MonoBehaviour
{
  // This is the type of outline you want to give all nerby objects
  [SerializeField] private Material outline_material;

  private XRDirectInteractor   direct_interactor;
  private XRInteractionManager interaction_manager;

  /*
   * Get direct interactor and interaction manager from hand object
   */
  void Awake()
  {
    direct_interactor = GetComponent<XRDirectInteractor>();

    interaction_manager = direct_interactor?.interactionManager;
  }

  /*
   * Get outline object from interactable
   *
   * PARAMS
   * - IXRInteractable interactable | The interactable from the interaction manager
   *
   * RETURN (GameObject outline_object)
   * - null | If the interactable does not have an outline object
   */
  private GameObject Get_Interactable_Outline_Object(IXRInteractable interactable)
  {
    return interactable?.transform.Find("Outline")?.gameObject;
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
   * Get the outline object for the object that the hand is targeting
   *
   * RETURN (GameObject outline_object)
   * - null | If the hand is not targeting any object
   */
  private GameObject Get_Target_Outline_Object()
  {
    List<IXRInteractable> interactables = new List<IXRInteractable>();

    interaction_manager?.GetValidTargets(direct_interactor, interactables);

    if(interactables.Count <= 0) return null;

    return Get_Interactable_Outline_Object(interactables[0]);
  }

  // Variable for keeping track of the last outlined object
  private GameObject last_outline_object;

  /*
   * Continuously add the outline material to the object that the hand is targeting
   * If the hand has selected an object, no object outline should be active
   */
  void Update()
  {
    // If the hand is holding an object, no object should be outlined
    if(direct_interactor.hasSelection)
    {
      last_outline_object?.SetActive(false);

      last_outline_object = null;
    }
    else // Outline just the object that the hand is targeting
    {
      GameObject outline_object = Get_Target_Outline_Object();

      Add_Outline_Material(outline_object);

      last_outline_object?.SetActive(false);

      outline_object?.SetActive(true);

      last_outline_object = outline_object;
    }
  }
}
