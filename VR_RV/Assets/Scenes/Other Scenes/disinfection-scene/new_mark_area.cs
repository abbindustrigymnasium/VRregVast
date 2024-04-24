using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class new_mark_area : MonoBehaviour
{
    [SerializeField] private int disinfectable_area_axis = 2;

    private bool inside_sphere = false;
    private List<GameObject> current_markers = new List<GameObject>();

    private GameObject mesh_object;
    private void Start()
    {
        mesh_object = new GameObject();
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (!collider.gameObject.CompareTag("Marker_Cube")) return;
        inside_sphere = true;
    }

    private void OnTriggerExit(Collider collider)
    {
        if (!collider.gameObject.CompareTag("Marker_Cube")) return;
        inside_sphere = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("Collided");
        // Skips the function if it's not only colliding with the area
        if (!collision.gameObject.CompareTag("Disinfectable")) return;
        
        if (inside_sphere) return;

        // Calculates wich side of the "rag" the marker should spawn
        float selected_axis_ofset = collision.gameObject.transform.position[disinfectable_area_axis] - transform.position[disinfectable_area_axis];
        float axis_sign_multiplier = Mathf.Sign(selected_axis_ofset);

        // Moves the spawning point of the marker to the edge of the cube that is colliding 
        Vector3 marker_position_vector = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        marker_position_vector[disinfectable_area_axis] += axis_sign_multiplier * transform.localScale[disinfectable_area_axis] / 2;

        // Creates a new marker, saves it to a list, and moves it to the correct position
        current_markers.Add(GameObject.CreatePrimitive(PrimitiveType.Cube));
        current_markers[current_markers.Count - 1].transform.position = marker_position_vector;
        current_markers[current_markers.Count - 1].tag = "Marker_Cube";
        current_markers[current_markers.Count - 1].GetComponent<BoxCollider>().isTrigger = true;
    }

    private Mesh AddObjectToMesh(GameObject object_to_mesh, Mesh previous_mesh)
    {
        
        Mesh combined_mesh = new Mesh();

        MeshFilter[] mesh_filters = new MeshFilter[] { object_to_mesh.GetComponent<MeshFilter>(), previous_mesh.GetComponent<MeshFilter>() };
        
        // Creates an array of combine instances for the gameObjects
        CombineInstance[] combiners = new CombineInstance[2];
        for(int a = 0; a < 2; a++)
        {
            combiners[a].subMeshIndex = 0;
            combiners[a].mesh = mesh_filters[a].sharedMesh;
            combiners[a].transform = mesh_filters[a].transform.localToWorldMatrix;
        }

        combined_mesh.CombineMeshes(combiners);

        return combined_mesh;
    }
}
