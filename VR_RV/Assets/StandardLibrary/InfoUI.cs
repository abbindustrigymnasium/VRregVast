

using System.Net.Mime;
using System.Security.AccessControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;


public class InfoUI : MonoBehaviour 
{
    public void Test()
    {
        transform.position = new Vector3(0, 5, 0);
    }

    
    public Sprite bunke;
    public TextMeshProUGUI title; 
    public TextMeshProUGUI text;
    public TextMeshProUGUI usage;

    public Image toolImage;
    public Transform camera;
    public Transform transform;    public Transform item;
    public Transform canvas;
    public bool isGrabbed = false;
    public bool isSelected = false;

    public PopUpText popUpText;
    public PopUpTitle popUpTitle;

    private string toolInfoPath = "Assets/data/toolInfo.json";

    public ToolInfo[] toolInfoList;


    public void Grab()
    {
        isGrabbed = true;
    }
    public void Release()
    {
        isGrabbed = false;
    }


    public void Select()
    {
        isSelected = true;
    }
    public void Deselect()
    {
        isSelected = false;
    }

    [System.Serializable]

    public class ToolInfo
    {
        public string title = "test";
        public string text;
        public string usage;
        public string imageUrl;

       
    }

     public ToolInfo getToolInfo(string s) // Get the tool info from the JSON string
        {
            Debug.Log(s);
            string[] tool = s.Replace("\"", "").Replace("[", "").Replace("]", "").Split(","); // Remove the quotes and brackets from the string
            if (tool.Length > 3)
            {
                ToolInfo toolInfo = new ToolInfo();
                toolInfo.title = tool[0].TrimStart(); // Remove leading spaces from title
                toolInfo.text = tool[1].TrimStart();
                toolInfo.usage = tool[3].TrimStart();
                toolInfo.imageUrl = tool[2].TrimStart();
                Debug.Log(toolInfo.imageUrl);
                return toolInfo;
            }

            if (tool.Length > 3)
            {
                ToolInfo toolInfo = new ToolInfo();
                toolInfo.title = tool[0];
                toolInfo.text = tool[1];
                toolInfo.usage = tool[3];
                toolInfo.imageUrl = tool[2];
                Debug.Log(toolInfo.imageUrl);
                return toolInfo;
            }
            else
            {
                return null;
            }



        }

    public ToolInfo[] getJson(string toolInfoPath) // Get the JSON data from the file
    {

        string jsonString = File.ReadAllText(toolInfoPath);
        Debug.Log(jsonString);
        string[] splitStrings = jsonString.Split("[");

        toolInfoList = new ToolInfo[splitStrings.Length]; // Initialize the toolInfoList array

        int i = 0;
        foreach (string s in splitStrings)
        {
            
            /*Debug.Log(s); */
            ToolInfo toolInfo = getToolInfo(s);
            
            if (toolInfo != null)
            {
                
                toolInfoList[i] = toolInfo; // Add the toolInfo to the toolInfoList array
                i++;
            }
        }

        return toolInfoList;

        // Use the jsonData dictionary as needed
    }

    public void closePopup() // Close the popup
    {
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
        ToolInfo[] toolInfoList = getJson(toolInfoPath);
        item = transform.parent.gameObject.transform; // get the parent transform of the UI element
        camera = Camera.main.transform; // get the camera transform
        
        transform.position = new Vector3(item.position.x + 1f, 2.0f, item.position.z);
        // Get the name of the parent object
        
        string parentObjectName = transform.parent.name; // Get the name of the parent object
        Debug.Log(parentObjectName);

        foreach (ToolInfo toolInfo in toolInfoList) // Loop through the toolInfoList array to find the toolInfo with the matching title
        {
            //Debug.Log(toolInfo.title);
         try {
            if (toolInfo.title != null && parentObjectName == toolInfo.title)
            {
                title.text = toolInfo.title;
                text.text = toolInfo.text;
                Debug.Log(toolInfo.imageUrl);
                bunke = Resources.Load<Sprite>(toolInfo.imageUrl);
                toolImage.sprite = bunke;
                usage.text = toolInfo.usage;
                break;
            }
            else {
                title.text = "test titleuhuhuh";
                text.text = "test textuhuhuh";
                usage.text = "test usageuhuhuh";
                bunke = Resources.Load<Sprite>("Sprites/Bunke"); // Load the sprite from the Resources folder 
                toolImage.sprite = bunke ; 
            }
         } catch (System.Exception e) {
             Debug.Log(e);
              title.text = "test title";
                text.text = "test text";
                usage.text = "test usage";
                toolImage.sprite = Resources.Load<Sprite>("Sprites/Bunke");

               
            
        }
       
    }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGrabbed && item != null)
        {
       
        Vector3 cameraPosition = camera.position;
        float radius = 1.5f;
        float speed = 0f;

        transform.position = new Vector3(cameraPosition.x + radius * Mathf.Sin(Time.time * speed), transform.position.y, cameraPosition.z + radius * Mathf.Cos(Time.time * speed));
        transform.LookAt(transform.position + transform.position - cameraPosition);
        }
    }

}