using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OutlineSelector : MonoBehaviour
{
  public  Material           outlineMaterial;
  private GameObject         outlineObject;
  private MeshRenderer       outlineRenderer;
  private XRGrabInteractable grabInteractable;

  private bool selected = false;

  void Awake()
  {
    grabInteractable = GetComponent<XRGrabInteractable>();

    // Create a new GameObject to display the outline material
    outlineObject = new GameObject("Outline");
    outlineObject.transform.SetParent(transform, false);

    // Copy the MeshFilter from the original object
    MeshFilter meshFilter = outlineObject.AddComponent<MeshFilter>();
    meshFilter.sharedMesh = GetComponent<MeshFilter>().sharedMesh;

    // Add a MeshRenderer and apply the outline material
    outlineRenderer = outlineObject.AddComponent<MeshRenderer>();
    outlineRenderer.material = outlineMaterial;

    // Initially disable the outline object
    outlineObject.SetActive(false);

    // Add listeners to hover events
    grabInteractable.hoverEntered.AddListener(OnHoverEnter);
    grabInteractable.hoverExited.AddListener(OnHoverExit);

    // Add listeners to select events
    grabInteractable.selectEntered.AddListener(OnSelectEnter);
    grabInteractable.selectExited.AddListener(OnSelectExit);
  }

  private void OnSelectEnter(SelectEnterEventArgs args)
  {
    selected = true;

    outlineObject.SetActive(false);
  }

  private void OnSelectExit(SelectExitEventArgs args)
  {
    selected = false;
  }

  private void OnHoverEnter(HoverEnterEventArgs args)
  {
    if(!selected) outlineObject.SetActive(true);
  }

  private void OnHoverExit(HoverExitEventArgs args)
  {
    if(!selected) outlineObject.SetActive(false);
  }
}
