using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class mesh_paint : MonoBehaviour
{
    [SerializeField] private Texture2D layer_mask_default;
    private Texture2D editable_layer_mask;
    private Color32[] default_state_pixel_array;
    private float percentage_colored;

    private float pixel_height;
    private float pixel_width;

    void Start()
    {

        // Get the material used on the quad
        Material disinfectable_area_material = GetComponent<Renderer>().material;

        // Create a new texture and apply the default texture to it
        editable_layer_mask = new Texture2D(layer_mask_default.width, layer_mask_default.height);
        default_state_pixel_array = layer_mask_default.GetPixels32();
        editable_layer_mask.SetPixels32(default_state_pixel_array);
        editable_layer_mask.Apply();

        // Set the input texture of the shader to the newly created texture
        disinfectable_area_material.SetTexture("_layer_mask", editable_layer_mask);

        // Calculate the relative size of 1 lenght unit compared to the disinfectable area
        float x_axis_scale = 1 / transform.localScale.x;
        float y_axis_scale = 1 / transform.localScale.y;

        // Calculate the width and height of 1 unit in pixels
        pixel_width = (x_axis_scale * layer_mask_default.width);
        pixel_height = (y_axis_scale * layer_mask_default.height);
    }

    void Update()
    {
        
        int colored_pixels = 0;
        
        // Get the color of each pixel of the edited layer mask 
        Color32[] comparable_pixel_array = editable_layer_mask.GetPixels32();
     
        // Compare the colors of the unedited and edited layer masks
        for (int i = 0; i < default_state_pixel_array.Length; i++)
        {
            // Increase the colored_pixels variables if the colors isn't the same
            if (comparable_pixel_array[i].g != default_state_pixel_array[i].g) colored_pixels++;
        }
        
        // Calculate the percentage of colored pixels
        percentage_colored = (colored_pixels * 100 / default_state_pixel_array.Length);
    }

    private void OnCollisionStay(Collision collision)
    {
        if(!collision.gameObject.CompareTag("Rag")) return;

        // Create a new array based on the amount of contacts
        int contact_points_amount = collision.contactCount;
        List<float> contact_points_in_pixels = new List<float>();
        ContactPoint[] used_points = new ContactPoint[contact_points_amount];
        
        // Set the values in the list
        for (int i = 0;  i < contact_points_amount * 2; i+=2)
        {
            // Get the current point of the itterable and skip using it if another point has the same coordinates
            ContactPoint current_contact = collision.GetContact(i/2);
            if (Array_Contains(used_points, current_contact)) continue;
            used_points[i/2] = current_contact;

            // Add the x and y position in pixels 
            contact_points_in_pixels.Add((current_contact.point.x + transform.localScale.x / 2) * pixel_width);
            contact_points_in_pixels.Add((current_contact.point.y + transform.localScale.y / 2) * pixel_height);
        }
        Debug.Log(contact_points_in_pixels.Count);

        // Create arrays of the x and y values in pixels
        float[] x_values_in_pixels = new float[contact_points_in_pixels.Count / 2];
        float[] y_values_in_pixels = new float[contact_points_in_pixels.Count / 2];
        for(int i = 0; i < contact_points_in_pixels.Count; i+=2) { 
            x_values_in_pixels[i / 2] = contact_points_in_pixels[i];
            y_values_in_pixels[i / 2] = contact_points_in_pixels[i + 1];
        }


        

        // Calculate starting point and max width and height
        int x_starting_value = (int)x_values_in_pixels.Min();
        int y_starting_value = (int)y_values_in_pixels.Min();
        
        int max_width_in_pixels = (int)x_values_in_pixels.Max() - x_starting_value;
        int max_height_in_pixels = (int)y_values_in_pixels.Max() - y_starting_value;
        
        /*string x_ps = "X: ";
        string y_ps = "Y: ";
        for (int i = 0; i < x_values_in_pixels.Length; i++) 
        {
            x_ps = x_ps + "[" + x_values_in_pixels[i] + "], ";
            y_ps = y_ps + "[" + y_values_in_pixels[i] + "], ";
        }
        Debug.Log(x_ps + "\n" + y_ps);*/

        // Set all the pixels within the width of the rag to black, starting in the bottom left
        for (int i = 0; i < max_width_in_pixels; i++)
        {
            // Calculate the x-axis position of the pixel to paint
            int x_pixel_position = x_starting_value + i;
            if (x_pixel_position < 0) x_pixel_position = 0;
            if (x_pixel_position >= layer_mask_default.width) x_pixel_position = layer_mask_default.width - 1;


            for (int j = 0; j < max_height_in_pixels; j++)
            {
                // Calculate the y-axis position of the pixel to paint
                int y_pixel_position = y_starting_value + j;
                if (y_pixel_position < 0) y_pixel_position = 0;
                if (y_pixel_position >= layer_mask_default.height) y_pixel_position = layer_mask_default.height - 1;
                
                if (Inside_Polygon(x_pixel_position, y_pixel_position, contact_points_in_pixels)) {
                    editable_layer_mask.SetPixel(x_pixel_position, y_pixel_position, Color.black); 
                }
            }
        }

        // Apply the changes
        editable_layer_mask.Apply();
        
    }
    
    bool Inside_Polygon(float x_value, float y_value, List<float> points)
    {
        float sum_of_angles = 0f;

        for (int i = 0; i < points.Count; i+=2)
        {
            float delta_x_1 = points[i + 2 < points.Count ? i + 2 : 0] - x_value;
            float delta_x_2 = points[i] - x_value;

            float delta_y_1 = points[i + 1 + 2 < points.Count ? i + 1 + 2 : 1] - y_value;
            float delta_y_2 = points[i + 1] - y_value;

            float theta_1 = Mathf.Atan2(delta_y_1, delta_x_1);
            float theta_2 = Mathf.Atan2(delta_y_2, delta_x_2);

            float delta_theta = theta_2 - theta_1;
            while (delta_theta > Mathf.PI) delta_theta -= Mathf.PI * 2;
            while (delta_theta < -Mathf.PI) delta_theta += Mathf.PI * 2;
            sum_of_angles += delta_theta;
        }
        if (Mathf.Abs(sum_of_angles) + 0.01f > Mathf.PI) return true;
        return false;
        
    }

    bool Array_Contains(ContactPoint[] contact_array, ContactPoint compared_contact)
    {
        for(int i = 0; i < contact_array.Length; i++)
        {
            Vector3 current_contact_point = contact_array[i].point;
            Vector3 compared_contact_point = compared_contact.point;
            if (current_contact_point.x == compared_contact_point.x && current_contact_point.y == compared_contact_point.y) return true;
        }
        return false;
    }
}
