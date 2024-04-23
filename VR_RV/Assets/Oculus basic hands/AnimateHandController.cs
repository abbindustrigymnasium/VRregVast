using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class AnimateHandController : MonoBehaviour
{
    public InputActionReference gripInputActionReference;
    public InputActionReference triggerInputActionReference;

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
        _grip_value = gripInputActionReference.action.ReadValue<float>();
        _hand_animator.SetFloat("Grip", _grip_value);
    }
    private void Animate_Trigger()
    {
        _trigger_value = triggerInputActionReference.action.ReadValue<float>();
        _hand_animator.SetFloat("Trigger", _trigger_value);
    }
}
