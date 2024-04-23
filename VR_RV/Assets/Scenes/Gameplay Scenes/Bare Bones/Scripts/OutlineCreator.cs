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
  public  Material           outlineMaterial;
  private XRDirectInteractor directInteractor;

  private List<GameObject> outlineObjects = new List<GameObject>();

  /*
   * Add listeners to hover and select events
   */
  void Awake()
  {
    directInteractor = GetComponent<XRDirectInteractor>();

    // Add listeners to hover events
    directInteractor.hoverEntered.AddListener(OnHoverEnter);
    directInteractor.hoverExited.AddListener(OnHoverExit);

    // Add listeners to select events
    directInteractor.selectEntered.AddListener(OnSelectEnter);
    directInteractor.selectExited.AddListener(OnSelectExit);
  }

  /*
   * Remove listeners to hover and select events
   */
  void onDestroy()
  {
    // Remove listeners to hover events
    directInteractor.hoverEntered.RemoveListener(OnHoverEnter);
    directInteractor.hoverExited.RemoveListener(OnHoverExit);

    // Remove listeners to select events
    directInteractor.selectEntered.RemoveListener(OnSelectEnter);
    directInteractor.selectExited.RemoveListener(OnSelectExit);
  }

  /*
   * Get outline object from interactable object
   *
   * PARAMS
   * - IXRInteractable interactableObject | The interactable object from XR direct interactor
   *
   * RETURN (GameObject outlineObject)
   */
  private GameObject GetOutlineObject(IXRInteractable interactableObject)
  {
    return interactableObject.transform.Find("Outline").gameObject;
  }

  /*
   * Add outline material to outline object mesh renderer
   *
   * PARAMS
   * - GameObject outlineObject | The outline object to add the material to
   */
  private void AddOutlineMaterial(GameObject outlineObject)
  {
    MeshRenderer outlineRenderer = outlineObject.GetComponent<MeshRenderer>();

    outlineRenderer.material = outlineMaterial;
  }

  /*
   * Inactivate all outlines around nerby outline objects
   */
  private void InactivateAllOutlines()
  {
    foreach(GameObject outlineObject in outlineObjects)
    {
      outlineObject.SetActive(false);
    }
  }

  /*
   * This method is called when the hand selects an object
   */
  private void OnSelectEnter(SelectEnterEventArgs args)
  {
    InactivateAllOutlines();

    // Remove all cached outline objects
    outlineObjects.Clear();
  }

  /*
   * Add material to the inputted outline object and add it to the list
   *
   * PARAMS
   * - GameObject outlineObject | The outline object to add
   */
  private void AddOutlineObject(GameObject outlineObject)
  {
    AddOutlineMaterial(outlineObject);

    outlineObjects.Add(outlineObject);
  }

  /*
   * This method is called when the hand releases an object
   *
   * When the hand releases an object, the object will still be in reach to select it again.
   *
   * Therefor you should add the object emidiatly to the outline objects list
   */
  private void OnSelectExit(SelectExitEventArgs args)
  {
    GameObject outlineObject = GetOutlineObject(args.interactableObject);

    AddOutlineObject(outlineObject);
  }

  /*
   * This method is called when the hand is nerby an object and can pick it up
   */
  private void OnHoverEnter(HoverEnterEventArgs args)
  {
    GameObject outlineObject = GetOutlineObject(args.interactableObject);

    AddOutlineObject(outlineObject);
  }

  /*
   * Get the index of the specified outline object in the list
   *
   * PARAMS
   * - GameObject outlineObject | The specified outline object
   *
   * RETURN (int index)
   */
  private int GetOutlineObjectIndex(GameObject outlineObject)
  {
    for(int index = 0; index < outlineObjects.Count; index++)
    {
      if(GameObject.ReferenceEquals(outlineObjects[index], outlineObject))
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
   * - GameObject outlineObject | The outline object to remove
   *
   * RETURN (bool result)
   */
  private bool RemoveOutlineObject(GameObject outlineObject)
  {
    int index = GetOutlineObjectIndex(outlineObject);

    if(index == -1) return false;

    outlineObject.SetActive(false);

    outlineObjects.RemoveAt(index);

    return true;
  }

  /*
   * This method is called when the hand is no longer in reach of a specific object
   */
  private void OnHoverExit(HoverExitEventArgs args)
  {
    GameObject outlineObject = GetOutlineObject(args.interactableObject);

    RemoveOutlineObject(outlineObject);
  }

  /*
   * Get the distance to a specific game object
   *
   * PARAMS
   * - GameObject gameObject
   *
   * RETURN (float distance)
   */
  private float GetGameObjectDistance(GameObject gameObject)
  {
    return Vector3.Distance(transform.position, gameObject.transform.position);
  }

  /*
   * Get the closest outline object to the hand
   *
   * RETURN (GameObject outlineObject)
   */
  private GameObject GetClosestOutlineObject()
  {
    if(outlineObjects.Count <= 0) return null;

    GameObject closestOutlineObject = outlineObjects[0];
    float      closestDistance      = GetGameObjectDistance(closestOutlineObject);

    for(int index = 1; index < outlineObjects.Count; index++)
    {
      GameObject outlineObject = outlineObjects[index];

      float distance = GetGameObjectDistance(outlineObject);

      if(distance < closestDistance)
      {
        closestOutlineObject = outlineObject;
        closestDistance      = distance;
      }
    }

    return closestOutlineObject;
  }

  /*
   * Draw outline around the closest object
   */
  private void DrawOutline()
  {
    // No outline has to be drawn if there are no objects
    if(outlineObjects.Count <= 0) return;

    // Inactivate all outlines and only draw an outline around the closest object 
    InactivateAllOutlines();

    GameObject closestOutlineObject = GetClosestOutlineObject();

    closestOutlineObject?.SetActive(true);
  }

  void Update()
  {
    DrawOutline();
  }
}
