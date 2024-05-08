using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoUI : MonoBehaviour
{
    public void Test() {
        transform.position = new Vector3(0, 5, 0);
    }

    public Transform camera;
    public Transform item;
    public bool isGrabbed = false;
    public bool isSelected = false;

    public void Grab() {
        isGrabbed = true;
    }
    public void Release() {
        isGrabbed = false;
    }
    

    public void Select() {
        isSelected = true;
    }
    public void Deselect() {
        isSelected = false;
    }
    
    // Start is called before the first frame update
    void Start()
    { 
        transform.position = new Vector3(item.position.x+1f, 2.0f, item.position.z);
    }


    // Update is called once per frame
    void Update()
    {
        if (!isGrabbed) {
        Vector3 direction = camera.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
        transform.position = new Vector3(item.position.x, transform.position.y, item.position.z);
        }
    }
}