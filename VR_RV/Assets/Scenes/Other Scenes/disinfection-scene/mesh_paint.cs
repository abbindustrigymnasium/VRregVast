using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class mesh_paint : MonoBehaviour
{

    [SerializeField] private Texture2D layer_mask_default;
    private Texture2D editable_layer_mask;
    private GameObject disinfectable_area;
    private Material disinfectable_area_material;
    private Color[] default_state_pixel_array;
    private float percentage_colored;

    void Start()
    {
        disinfectable_area = GameObject.FindGameObjectWithTag("Disinfectable");
        disinfectable_area_material = disinfectable_area.GetComponent<Renderer>().material;

        default_state_pixel_array = layer_mask_default.GetPixels();

        editable_layer_mask = new Texture2D(layer_mask_default.width, layer_mask_default.height);
        editable_layer_mask.SetPixels(layer_mask_default.GetPixels());
        editable_layer_mask.Apply();
        disinfectable_area_material.SetTexture("_layer_mask", editable_layer_mask);

    }

    // Update is called once per frame
    void Update()
    {
        int colored_pixles = 0;
        
        // Get the color of each pixel of the edited layer mask 
        Color[] comparable_pixel_array = editable_layer_mask.GetPixels();
     
        // Compare the colors of the unedited and edited layer masks
        for (int i = 0; i < default_state_pixel_array.Length; i++)
        {
            // Increase the colored_pixels variables if the colors isn't the same
            if (comparable_pixel_array[i] != default_state_pixel_array[i]) colored_pixles++;
        }

        // Calculate the percentage of colored pixles
        percentage_colored = colored_pixles / default_state_pixel_array.Length;
        Debug.Log(colored_pixles);
    }

    private void OnCollisionStay(Collision collision)
    {
        if(!collision.gameObject.CompareTag("Disinfectable")) return;

        // Calculate the relative size of the rag compared to the disinfectable area
        float x_axis_scale = transform.localScale.x / collision.transform.localScale.x;
        float y_axis_scale = transform.localScale.y / collision.transform.localScale.y;

        // Calculate the width and height of the rag in pixles
        int pixel_width = (int)(x_axis_scale * layer_mask_default.width);
        int pixel_height = (int)(y_axis_scale * layer_mask_default.height);

        // Calculate the relative position of the rag to the disinfectable object
        Vector3 relative_position = transform.position - collision.gameObject.transform.position;

        // Calculate the start position to color on the layer mask in pixels
        int[] pixel_start = new int[2];
        pixel_start[0] = (int) ((relative_position.x + collision.gameObject.transform.localScale.x / 2) * pixel_width - transform.localScale.x * pixel_width / 2);
        pixel_start[1] = (int) ((relative_position.y + collision.gameObject.transform.localScale.y / 2) * pixel_height - transform.localScale.y * pixel_height / 2);

        // Set all the pixles within the width of the rag to black, starting in the bottom left
        for (int i = 0; i < pixel_width; i++)
        {
            for(int j = 0; j < pixel_height; j++)
            {
                editable_layer_mask.SetPixel(pixel_start[0] + i,pixel_start[1] + j,Color.black); 
            }
        }

        // Apply the changes
        editable_layer_mask.Apply();
        
    }
    
    
}
