using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class mesh_paint : MonoBehaviour
{

    [SerializeField] private Texture2D dirt_mask_base;
    private Texture2D dirt_mask;
    private GameObject disinfectable_area;
    private Material disinfectable_area_material;

    private Vector3 relative_position;

    void Start()
    {
        disinfectable_area = GameObject.FindGameObjectWithTag("Disinfectable");
        disinfectable_area_material = disinfectable_area.GetComponent<Renderer>().material;

        dirt_mask = new Texture2D(dirt_mask_base.width, dirt_mask_base.height);
        dirt_mask.SetPixels(dirt_mask_base.GetPixels());
        dirt_mask.Apply();
        disinfectable_area_material.SetTexture("_dirt_mask", dirt_mask);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if(!collision.gameObject.CompareTag("Disinfectable")) return;
        float x_axis_scale = transform.localScale.x / collision.transform.localScale.x;
        float y_axis_scale = transform.localScale.y / collision.transform.localScale.y;

        // Convert the relative size to pixel size on layer mask
        int pixel_width = (int)(x_axis_scale * dirt_mask_base.width);
        int pixel_height = (int)(y_axis_scale * dirt_mask_base.height);

        relative_position = transform.position - collision.gameObject.transform.position;

        // Calculate the start position on the layer mask in pixels
        int[] pixel_start = new int[2];
        pixel_start[0] = (int) ((relative_position.x + collision.gameObject.transform.localScale.x / 2) * pixel_width - transform.localScale.x * pixel_width / 2);
        pixel_start[1] = (int) ((relative_position.y + collision.gameObject.transform.localScale.y / 2) * pixel_height - transform.localScale.y * pixel_height / 2);


        for (int i = 0; i < pixel_width; i++)
        {
            for(int j = 0; j < pixel_height; j++)
            {
                dirt_mask.SetPixel(pixel_start[0] + i,pixel_start[1] + j,Color.black); 
            }
        }
        dirt_mask.Apply();
    }
    
    
}
