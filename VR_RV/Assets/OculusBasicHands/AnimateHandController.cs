using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class AnimateHandController : MonoBehaviour
{
    public InputActionReference grip_input_action_reference;
    public InputActionReference trigger_input_action_reference;

    private Animator _hand_animator;
    private float _grip_value;
    private float _trigger_value;

    private void Start()
    {
        _hand_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Animate_Grip();
        Animate_Trigger();
    }
    private void Animate_Grip()
    {
        _grip_value = grip_input_action_reference.action.ReadValue<float>();
        _hand_animator.SetFloat("Grip", _grip_value);
    }
    private void Animate_Trigger()
    {
        _trigger_value = trigger_input_action_reference.action.ReadValue<float>();
        _hand_animator.SetFloat("Trigger", _trigger_value);
    }
}
