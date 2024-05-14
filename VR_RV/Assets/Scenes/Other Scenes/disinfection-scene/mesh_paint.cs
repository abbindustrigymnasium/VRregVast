using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mesh_paint : MonoBehaviour
{

    [SerializeField] private Texture2D base_layer;
    // Start is called before the first frame update
    void Start()
    {
        
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

        int x_width = (int)(x_axis_scale * base_layer.width);
        int y_height = (int)(y_axis_scale * base_layer.height);

    }
}
