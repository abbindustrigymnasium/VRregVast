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
  public GameObject main_object;

  public GameObject move_object;

  [SerializeField]
  private GameObject main_pivot_object;

  [SerializeField]
  private GameObject move_pivot_object;

  [SerializeField]
  private float max_angle = 20;

  public float activation;


  private ParentConstraint move_constraint;

  private ParentConstraint pivot_constraint;

  private float last_activation;


  private ConstraintSource Constraint_Source_Create(Transform source_transform)
  {
    ConstraintSource constraint_source = new ConstraintSource();

    constraint_source.weight = 1;

    constraint_source.sourceTransform = source_transform;

    return constraint_source;
  }

  private void Pivot_Constraint_Create()
  {
    pivot_constraint = move_pivot_object.AddComponent<ParentConstraint>();

    ConstraintSource constraint_source = Constraint_Source_Create(main_pivot_object.transform);

    pivot_constraint.AddSource(constraint_source);

    pivot_constraint.constraintActive = true;
  }

  private void Move_Constraint_Create()
  {
    move_constraint = move_object.AddComponent<ParentConstraint>();

    ConstraintSource constraint_source = Constraint_Source_Create(move_pivot_object.transform);

    move_constraint.AddSource(constraint_source);

    Vector3 position_offset = move_pivot_object.transform.position - move_object.transform.position;

    move_constraint.SetTranslationOffset(0, position_offset);

    move_constraint.constraintActive = true;
  }

  void Start()
  {
    Pivot_Constraint_Create();

    Move_Constraint_Create();

    pivot_constraint.SetRotationOffset(0, new Vector3(0, 0, 0));

    ToolCombineMeshes tool_combine_meshes = GetComponent<ToolCombineMeshes>();

    if(tool_combine_meshes) tool_combine_meshes.UpdateMesh();
  }

  void Update()
  {
    if(activation != last_activation)
    {
      float angle = Mathf.Lerp(0, max_angle, activation);

      pivot_constraint.SetRotationOffset(0, new Vector3(0, -angle, 0));

      last_activation = activation;
    }
  }
}
