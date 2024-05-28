/*
 * This script should be added to each tool that can be activated
 *
 * Written by Hampus Fridholm
 *
 * 2024-05-28
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class ToolActivation : MonoBehaviour
{
  [SerializeField]
  private GameObject main_object;

  [SerializeField]
  private GameObject move_object;

  [SerializeField]
  private Transform pivot_transform;

  [SerializeField]
  private float max_angle = 20;

  public float activation;

  private ParentConstraint constraint;

  void Awake()
  {
    constraint = move_object.AddComponent<ParentConstraint>();

    Vector3 position_offset = pivot_transform.position - move_object.transform.position;

    ConstraintSource constraint_source = new ConstraintSource();

    constraint_source.weight = 1;
    constraint_source.sourceTransform = pivot_transform;

    constraint.AddSource(constraint_source);

    constraint.SetTranslationOffset(0, position_offset);

    constraint.constraintActive = true;
  }

  void Update()
  {
    float angle = Mathf.Lerp(0, max_angle, activation);

    constraint.SetRotationOffset(0, new Vector3(0, -angle, 0));
  }
}
