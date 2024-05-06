using UnityEngine;

public class track_area_no_overlap : MonoBehaviour
{
    [SerializeField] private int columns;
    [SerializeField] private int disinfectable_area_normal_axis = 2;

    private void Start()
    {
        GameObject disinfectable_area = GameObject.FindGameObjectWithTag("Disinfectable");
        
        // Gets y-axis scale if z-axis is the normal and vice versa
        int width_axis = disinfectable_area_normal_axis == 2 ? 0 : 2;

        // Calculates size of the columns
        float bounds_height = disinfectable_area.transform.localScale.y;
        float disinfectable_area_width = disinfectable_area.transform.localScale[width_axis];
        float bounds_width = disinfectable_area_width / columns;
        
        // Calculate the starting point ofset for the bounds
        float left_starting_point = disinfectable_area.transform.position[width_axis] - disinfectable_area_width / 2 + bounds_width / 2;


        GameObject[] bounds = new GameObject[columns];
        for(int i = 0; i < columns; i++)
        {
            // Calculates position of the bound box
            Vector3 bound_position = disinfectable_area.transform.position;
            bound_position[width_axis] = left_starting_point + bounds_width * i;

            // Calculates the scale of the bound box
            Vector3 bound_scale = disinfectable_area.transform.localScale;
            bound_scale[width_axis] = bounds_width;
            bound_scale.y = bounds_height;

            // Spawns the box and adjusts its position and scale
            bounds[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
            bounds[i].transform.position = bound_position;
            bounds[i].transform.localScale = bound_scale;
        }
    }
    private void OnCollisionStay(Collision collision)
    {   
        // Skips if the rag isn't only colliding with the right object
        if (!collision.gameObject.CompareTag("Disinfectable")) return;
        if (Physics.OverlapBox(transform.position, transform.localScale / 2, Quaternion.identity).Length > 2) return;

        // Create an array of contact points
        int contact_point_count = collision.contactCount;
        Vector3[] contact_points = new Vector3[contact_point_count];
        for(int i = 0; i < contact_point_count; i++) contact_points[i] = collision.contacts[i].point;

        // Calculate center
        Vector3 center = new Vector3();
        for(int i = 0; i < 3; i++) for (int j = 0; j < contact_point_count; j++) center[i] += contact_points[j][i];
        center /= contact_point_count;

        // Spawn object on center
        GameObject ball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        ball.transform.position = center;
        ball.GetComponent<SphereCollider>().isTrigger = true;
        ball.transform.localScale = new Vector3(2, 2, 2);

        Debug.Log(center);
    }
}
