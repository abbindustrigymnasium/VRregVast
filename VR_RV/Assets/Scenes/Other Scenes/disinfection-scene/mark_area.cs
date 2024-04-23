using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mark_area : MonoBehaviour {

    private List<GameObject> marker_spheres = new List<GameObject>();
    private Collider[] current_collisions;

    private float half_z_axis_lenght;
    
    void Start () {
        half_z_axis_lenght = gameObject.transform.localScale.z / 2;
    }
    void Update() {
        current_collisions = Physics.OverlapSphere(transform.position, 1f);

        // Returns if the "rag" is not only colliding with the disinfectable area
        if (current_collisions.Length != 1) return;
        if (current_collisions[0].tag != "Disinfectable") return;

        Debug.Log("Colldided");
        // Gets the sign of the difference in position
        float z_axis_ofset = current_collisions[0].transform.position.z - gameObject.transform.position.z;
        float z_axis_multiplier = Mathf.Sign(z_axis_ofset);

        // Adds sphere on collision position
        marker_spheres.Add(GameObject.CreatePrimitive(PrimitiveType.Sphere));
        marker_spheres[marker_spheres.Count - 1].transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + (half_z_axis_lenght *  z_axis_multiplier));
    }

}
