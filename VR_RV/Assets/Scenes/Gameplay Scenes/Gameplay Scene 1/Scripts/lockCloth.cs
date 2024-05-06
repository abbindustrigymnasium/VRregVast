using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lockCloth : MonoBehaviour
{
    public bool freezeRigidbody = false; // Variable to determine whether to freeze the rigidbody or not

    public Rigidbody rb; // Reference to the Rigidbody component

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component attached to this GameObject
    }

    // Update is called once per frame
    void Update()
    {
        if (freezeRigidbody)
        {
            // If freezeRigidbody is true, freeze the Rigidbody
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        else
        {
            // If freezeRigidbody is false, unfreeze the Rigidbody
            rb.constraints = RigidbodyConstraints.None;
        }
    }
}
