
//using System.Diagnostics;
using System.Net.Mime;
using System.Security.AccessControl;
//using System.Diagnostics;
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
        public string title;
        public string text;
        public string usage;
        public string imageUrl;

        public static ToolInfo getToolInfo(string s) // Get the tool info from the JSON string
        {
            string[] tool = s.Split(",");

            if (tool.Length > 3)
            {
                for (int i = 0; i < 3; i++)
                {
                    tool[i] = s.Split(',')[i].Replace("\"", "");

                }
                ToolInfo toolInfo = new ToolInfo();
                toolInfo.title = tool[0];
                toolInfo.text = tool[1];
                toolInfo.usage = tool[3];
                toolInfo.imageUrl = tool[2];
                Debug.Log(tool[0]);
                return toolInfo;
            }
            else
            {
                return null;
            }



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
            i++;

            /*Debug.Log(s); */
            ToolInfo toolInfo = ToolInfo.getToolInfo(s);
            if (toolInfo != null)
            {
                toolInfoList[i - 1] = toolInfo; // Add the toolInfo to the toolInfoList array
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
        
         try {
            if (toolInfo.title != null && parentObjectName == toolInfo.title)
            {
                title.text = toolInfo.title;
                text.text = toolInfo.text;
                toolImage.sprite = Resources.Load<Sprite>(toolInfo.imageUrl);
                usage.text = toolInfo.usage;
            }
            else {
                title.text = "test title";
                text.text = "test text";
                usage.text = "test usage";
                toolImage.sprite = Resources.Load<Sprite>("Assets/Pictures/Bunke.jpg");
            }
         } catch (System.Exception e) {
             Debug.Log(e);
              title.text = "test title";
                text.text = "test text";
                usage.text = "test usage";
                toolImage.sprite = Resources.Load<Sprite>("Assets/Pictures/Bunke.jpg");
            
        }
       
    }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGrabbed && item != null)
        {
            
            Vector3 direction = camera.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            canvas.rotation = rotation;
            transform.position = new Vector3(item.position.x, 3, item.position.z);
        
        }
    }

}