using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ObjectDropped : MonoBehaviour
{
    [SerializeField] private int score_penalty;

    public event Action<int> OnItemDropped;

    void OnCollisionEnter(Collision collided_collider)
    {
        if (collided_collider.gameObject.tag == "Interactable")
        {
            OnItemDropped?.Invoke(score_penalty);
        }
    }
}
