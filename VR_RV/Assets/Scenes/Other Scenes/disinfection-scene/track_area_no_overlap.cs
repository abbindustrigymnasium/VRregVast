using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;

public class track_area_no_overlap : MonoBehaviour
{
    private void OnCollisionStay(Collision collision)
    {   
        // Only runs if the rag is only colliding with the right object
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

        GameObject ball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        ball.transform.position = center;
        ball.GetComponent<SphereCollider>().isTrigger = true;
        ball.transform.localScale = new Vector3(2, 2, 2);

        Debug.Log(center);
    }
}
