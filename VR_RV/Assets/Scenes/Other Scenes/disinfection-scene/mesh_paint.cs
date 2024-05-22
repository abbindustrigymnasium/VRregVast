using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class mesh_paint : MonoBehaviour
{
    //[SerializeField] private string front_facing_axis;
    private int front_facing_index;

    [SerializeField] private Texture2D layer_mask_default;
    private Texture2D editable_layer_mask;
    private Color32[] default_state_pixel_array;
    private float percentage_colored;

    void Start()
    {
        // Set the axis based on input
        /*switch (front_facing_axis) {
            case "x":
                front_facing_index = 0; break;
            case "y":
                front_facing_index = 1; break;
            case "z":
                front_facing_index = 2; break;
            default:
                front_facing_index = 2; break;
        }*/

        // Get the material used on the quad
        Material disinfectable_area_material = GetComponent<Renderer>().material;

        // Create a new texture and apply the default texture to it
        editable_layer_mask = new Texture2D(layer_mask_default.width, layer_mask_default.height);
        default_state_pixel_array = layer_mask_default.GetPixels32();
        editable_layer_mask.SetPixels32(default_state_pixel_array);
        editable_layer_mask.Apply();

        // Set the input texture of the shader to the newly created texture
        disinfectable_area_material.SetTexture("_layer_mask", editable_layer_mask);

    }

    void Update()
    {
        
        int colored_pixles = 0;
        
        // Get the color of each pixel of the edited layer mask 
        Color32[] comparable_pixel_array = editable_layer_mask.GetPixels32();
     
        // Compare the colors of the unedited and edited layer masks
        for (int i = 0; i < default_state_pixel_array.Length; i++)
        {
            // Increase the colored_pixels variables if the colors isn't the same
            if (comparable_pixel_array[i].g != default_state_pixel_array[i].g) colored_pixles++;
        }
        
        // Calculate the percentage of colored pixles
        percentage_colored = (colored_pixles * 100 / default_state_pixel_array.Length);
    }

    private void OnCollisionStay(Collision collision)
    {
        if(!collision.gameObject.CompareTag("Rag")) return;

        // Calculate the relative size of the rag compared to the disinfectable area
        float x_axis_scale = collision.transform.localScale.x / transform.localScale.x;
        float y_axis_scale = collision.transform.localScale.y / transform.localScale.y;

        // Calculate the width and height of the rag in pixles
        float pixel_width = (x_axis_scale * layer_mask_default.width);
        float pixel_height = (y_axis_scale * layer_mask_default.height);

        // Calculate the relative position of the rag to the disinfectable object
        Vector3 relative_position = collision.transform.position - transform.position;

        // Calculate the start position to color on the layer mask in pixels
        int[] pixel_start = new int[2];
        pixel_start[0] = (int) ((relative_position.x + transform.localScale.x / 2) * pixel_width - collision.transform.localScale.x * pixel_width / 2);
        pixel_start[1] = (int) ((relative_position.y + transform.localScale.y / 2) * pixel_height - collision.transform.localScale.y * pixel_height / 2);

        // Set all the pixles within the width of the rag to black, starting in the bottom left
        for (int i = 0; i < pixel_width; i++)
        {
            // Calculate the x-axis position of the pixel to paint
            int x_pixel_position = pixel_start[0] + i;
            if (x_pixel_position < 0) x_pixel_position = 0;
            if (x_pixel_position >= layer_mask_default.width) x_pixel_position = layer_mask_default.width - 1;


            for (int j = 0; j < pixel_height; j++)
            {
                // Calculate the y-axis position of the pixel to paint
                int y_pixel_position = pixel_start[1] + j;
                if (y_pixel_position < 0) y_pixel_position = 0;
                if (y_pixel_position >= layer_mask_default.height) y_pixel_position = layer_mask_default.height - 1;

                editable_layer_mask.SetPixel(x_pixel_position, y_pixel_position, Color.black); 
            }
        }

        // Apply the changes
        editable_layer_mask.Apply();
        
    }
    
    bool Inside_Polygon(int x_value, int y_value, int[] points)
    {
        float sum_of_angles = 0f;

        for (int i = 0; i < points.Length; i+=2)
        {
            float delta_x_1 = points[i + 2 < points.Length ? i + 2 : 0] - x_value;
            float delta_x_2 = points[i] - x_value;

            float delta_y_1 = points[i + 1 + 2 < points.Length ? i + 1 + 2 : 1] - y_value;
            float delta_y_2 = points[i + 1] - y_value;

            float theta_1 = Mathf.Atan2(delta_y_1, delta_x_1);
            float theta_2 = Mathf.Atan2(delta_y_2, delta_x_2);

            float delta_theta = theta_2 - theta_1;
            while (delta_theta > Mathf.PI) delta_theta -= Mathf.PI * 2;
            while (delta_theta < -Mathf.PI) delta_theta += Mathf.PI * 2;
            sum_of_angles += delta_theta;
        }
        if (Mathf.Abs(sum_of_angles) == 0) return false;
        return true;



        
    }
}