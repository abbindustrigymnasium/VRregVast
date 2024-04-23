using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mark_area : MonoBehaviour {

    private List<GameObject> marker_spheres = new List<GameObject>();
    private Collider[] current_collisions;

    private float half_z_axis_lenght;
    
    private void Start () {
        half_z_axis_lenght = gameObject.transform.localScale.z / 2;
    }
    private void Update() {
        current_collisions = Physics.OverlapBox(transform.position, new Vector3(0f, 0f, transform.localScale.z / 2));
        
        // Returns if the "rag" is not only colliding with the disinfectable area
        if (current_collisions.Length != 1) return;
        if (current_collisions[0].tag != "Disinfectable") return;

        // Gets the sign of the difference in position
        float z_axis_ofset = current_collisions[0].transform.position.z - gameObject.transform.position.z;
        float z_axis_multiplier = Mathf.Sign(z_axis_ofset);

        // Adds sphere on collision position
        marker_spheres.Add(GameObject.CreatePrimitive(PrimitiveType.Sphere));
        marker_spheres[marker_spheres.Count - 1].transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, half_z_axis_lenght *  z_axis_multiplier);
    }

}
