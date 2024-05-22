using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class mesh_paint : MonoBehaviour
{

    [SerializeField] private Texture2D layer_mask_default;
    private Texture2D editable_layer_mask;
    private Color32[] default_state_pixel_array;
    private float percentage_colored;

    void Start()
    {

        Material disinfectable_area_material = GetComponent<Renderer>().material;

        editable_layer_mask = new Texture2D(layer_mask_default.width, layer_mask_default.height);

        default_state_pixel_array = layer_mask_default.GetPixels32();
        editable_layer_mask.SetPixels32(default_state_pixel_array);
        editable_layer_mask.Apply();

        disinfectable_area_material.SetTexture("_layer_mask", editable_layer_mask);

    }

    // Update is called once per frame
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
        int pixel_width = (int)(x_axis_scale * layer_mask_default.width);
        int pixel_height = (int)(y_axis_scale * layer_mask_default.height);

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
    
    
}
