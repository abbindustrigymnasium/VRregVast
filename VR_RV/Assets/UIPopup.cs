using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPopup : MonoBehaviour
{
    public GameObject prefabToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        OpenPopup();
    }

    public void OpenPopup()
    {
        GameObject popup = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
        popup.transform.SetParent(transform); // set the parent of the popup to the UIPopup
        popup.transform.localPosition = Vector3.zero;
    
    }

    public void ClosePopup()
    {
        Destroy(transform.GetChild(0).gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
