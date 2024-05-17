using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class mesh_paint : MonoBehaviour
{

    [SerializeField] private Texture2D dirt_mask_base;
    private Texture2D dirt_mask;
    private GameObject disinfectable_area;
    private Material disinfectable_area_material;

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

        int x_width = (int)(x_axis_scale * dirt_mask_base.width);
        int y_height = (int)(y_axis_scale * dirt_mask_base.height);


        for (int i = 0; i < x_width; i++)
        {
            for(int j = 0; j < y_height; j++)
            {
                dirt_mask.SetPixel(i,j,Color.black); 
            }
        }
        dirt_mask.Apply();
    }
    
}
