/*
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
  private IEnumerator Add_Outline_Material(GameObject outline_object)
  {
    MeshRenderer outline_renderer = outline_object?.GetComponent<MeshRenderer>();

    if(outline_renderer)
    {
      Material fading_material = new Material(outline_material);

      outline_renderer.material = fading_material;

      outline_object?.SetActive(true);

      yield return Fade_Outline_Material(fading_material, true, 5f);
    }
  }

  /*
   *
   */
  private IEnumerator Remove_Outline_Material(GameObject outline_object)
  {
    MeshRenderer outline_renderer = outline_object?.GetComponent<MeshRenderer>();

    if(outline_renderer)
    {
      Material fading_material = outline_renderer.material;

      yield return Fade_Outline_Material(fading_material, false, 5f);

      outline_object?.SetActive(false);
    }
  }
  
  /*
   *
   */
  private IEnumerator Fade_Outline_Material(Material fading_material, bool fade_in, float duration)
  {
    Color fading_color = fading_material.color;

    for(float counter = 0; counter < duration; counter += Time.deltaTime)
    {
      float a = (fade_in ? 1 : 0);
      float b = (fade_in ? 0 : 1);

      float alpha = Mathf.Lerp(a, b, counter / duration);

      fading_material.color = new Color(fading_color.r, fading_color.g, fading_color.b, alpha);

      yield return null;
    }
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
      StartCoroutine(Remove_Outline_Material(last_outline_object));

      last_outline_object = null;
    }
    else // Outline just the object that the hand is targeting
    {
      GameObject outline_object = Get_Target_Outline_Object();

      StartCoroutine(Add_Outline_Material(outline_object));

      StartCoroutine(Remove_Outline_Material(last_outline_object));

      last_outline_object = outline_object;
    }
  }
}
