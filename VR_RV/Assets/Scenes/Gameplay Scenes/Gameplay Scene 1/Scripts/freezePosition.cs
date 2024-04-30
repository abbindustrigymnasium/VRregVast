using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class freezePosition : MonoBehaviour
{
    public Rigidbody rb;
    public bool bollean;

    // Start is called before the first frame update
    void Start()
    {
        if (rb == null)
        {
            // If not, log an error message
            Debug.LogError("Rigidbody is not assigned to the RigidbodyDragger script!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (bollean == true)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
