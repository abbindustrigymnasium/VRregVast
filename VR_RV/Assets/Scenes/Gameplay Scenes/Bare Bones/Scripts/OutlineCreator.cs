/*
 * This script should be added to each hand that is going to interact with objects
 *
 * Written by Hampus Fridholm
 *
 * 2024-04-23
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OutlineCreator : MonoBehaviour
{
  // This is the type of outline you want to give all nerby objects
  public  Material           outline_material;
  private XRDirectInteractor direct_interactor;

  private List<GameObject> outline_objects = new List<GameObject>();

  // Instead of inactivating every other outline except the closest,
  // which created a bug, where one hands closest object would be inactivated by the other hand,
  // only the last closest object will be inactivated
  private GameObject last_closest_outline_object;

  /*
   * Add listeners to hover and select events
   */
  void Awake()
  {
    direct_interactor = GetComponent<XRDirectInteractor>();

    // Add listeners to hover events
    direct_interactor.hoverEntered.AddListener(On_Hover_Enter);
    direct_interactor.hoverExited.AddListener(On_Hover_Exit);

    // Add listeners to select events
    direct_interactor.selectEntered.AddListener(On_Select_Enter);
    direct_interactor.selectExited.AddListener(On_Select_Exit);
  }

  /*
   * Remove listeners to hover and select events
   */
  void onDestroy()
  {
    // Remove listeners to hover events
    direct_interactor.hoverEntered.RemoveListener(On_Hover_Enter);
    direct_interactor.hoverExited.RemoveListener(On_Hover_Exit);

    // Remove listeners to select events
    direct_interactor.selectEntered.RemoveListener(On_Select_Enter);
    direct_interactor.selectExited.RemoveListener(On_Select_Exit);
  }

  /*
   * Get outline object from interactable object
   *
   * PARAMS
   * - IXRInteractable interactable_object | The interactable object from XR direct interactor
   *
   * RETURN (GameObject outline_object)
   */
  private GameObject Get_Outline_Object(IXRInteractable interactable_object)
  {
    return interactable_object.transform.Find("Outline").gameObject;
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
   * This method is called when the hand selects an object
   */
  private void On_Select_Enter(SelectEnterEventArgs args)
  {
    last_closest_outline_object?.SetActive(false);

    // Remove all cached outline objects
    outline_objects.Clear();
  }

  /*
   * Add material to the inputted outline object and add it to the list
   *
   * PARAMS
   * - GameObject outline_object | The outline object to add
   */
  private void Add_Outline_Object(GameObject outline_object)
  {
    Add_Outline_Material(outline_object);

    outline_objects.Add(outline_object);
  }

  /*
   * This method is called when the hand releases an object
   *
   * When the hand releases an object, the object will still be in reach to select it again.
   *
   * Therefor you should add the object emidiatly to the outline objects list
   */
  private void On_Select_Exit(SelectExitEventArgs args)
  {
    GameObject outline_object = Get_Outline_Object(args.interactableObject);

    Add_Outline_Object(outline_object);
  }

  /*
   * This method is called when the hand is nerby an object and can pick it up
   */
  private void On_Hover_Enter(HoverEnterEventArgs args)
  {
    GameObject outline_object = Get_Outline_Object(args.interactableObject);

    Add_Outline_Object(outline_object);
  }

  /*
   * Get the index of the specified outline object in the list
   *
   * PARAMS
   * - GameObject outline_object | The specified outline object
   *
   * RETURN (int index)
   */
  private int Get_Outline_Object_Index(GameObject outline_object)
  {
    for(int index = 0; index < outline_objects.Count; index++)
    {
      if(GameObject.ReferenceEquals(outline_objects[index], outline_object))
      {
        return index;
      }
    }
    return -1;
  }

  /*
   * Remove the specified outline object from the list and inactivate it
   *
   * PARAMS
   * - GameObject outline_object | The outline object to remove
   *
   * RETURN (bool result)
   */
  private bool Remove_Outline_Object(GameObject outline_object)
  {
    int index = Get_Outline_Object_Index(outline_object);

    if(index == -1) return false;

    outline_object.SetActive(false);

    outline_objects.RemoveAt(index);

    return true;
  }

  /*
   * This method is called when the hand is no longer in reach of a specific object
   */
  private void On_Hover_Exit(HoverExitEventArgs args)
  {
    GameObject outline_object = Get_Outline_Object(args.interactableObject);

    Remove_Outline_Object(outline_object);
  }

  /*
   * Get the distance to a specific game object
   *
   * PARAMS
   * - GameObject game_object
   *
   * RETURN (float distance)
   */
  private float Get_Game_Object_Distance(GameObject game_object)
  {
    return Vector3.Distance(transform.position, game_object.transform.position);
  }

  /*
   * Get the closest outline object to the hand
   *
   * RETURN (GameObject outline_object)
   */
  private GameObject Get_Closest_Outline_Object()
  {
    if(outline_objects.Count <= 0) return null;

    GameObject closest_outline_object = outline_objects[0];
    float      closest_distance       = Get_Game_Object_Distance(closest_outline_object);

    for(int index = 1; index < outline_objects.Count; index++)
    {
      StartCoroutine(Remove_Outline_Material(last_outline_object));

      last_outline_object = null;
    }
    else // Outline just the object that the hand is targeting
    {
      GameObject outline_object = Get_Target_Outline_Object();

      StartCoroutine(Add_Outline_Material(outline_object));

      StartCoroutine(Remove_Outline_Material(last_outline_object));

      last_closest_outline_object = closest_outline_object;
    }

    closest_outline_object?.SetActive(true);
  }

  void Update()
  {
    Draw_Outline();
  }
}
