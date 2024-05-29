using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

//This code was writen by Olle Östman and Simon Meier

public class GiveItems : MonoBehaviour
{
    private Audio audioManager;
    // The parent that contains all the Items the doctor will be asking for
    public Transform ParentOfItems;
    // Used to change to the next item
    // private int Request = 0;
    // Makes it easier to accses the child the doctor wants
    private GameObject Child;
    // The textbox that displays what the doctor wants
    public TMP_Text TextBox;

    private bool truth = false;

    public GameObject Socket;



    // Updates child at the start and checks if and how many children ParentOfItems has
    void Start()
    {
        ChildUpdate();
        if (ParentOfItems == null)
        {
            Debug.LogError("ParentOfItems is not assigned in the inspector.");
            return;
        }

        if (ParentOfItems.childCount == 0)
        {
            Debug.LogError("ParentOfItems has no children.");
            return;
        }

        for (int i = 0; i < ParentOfItems.childCount; i++)
        {
            Debug.Log(ParentOfItems.GetChild(i).gameObject);
        }

        Debug.Log("ParentOfItems is assigned and has " + ParentOfItems.childCount + " children.");
    }

    // Updates to next child from the parent object and changes text to ask for that item
    public void ChildUpdate()
    {
        if (ParentOfItems.childCount == 0)
        {
            TextBox.text = "Använd dammsugaren för att suga upp blodet!";
            truth = true;
            GameObject.Find("Task Manager").GetComponentInChildren<FinishedGivingObjectsScript>().Done();
        }
        else
        {
            Child = ParentOfItems.GetChild(0).gameObject;
            TextBox.text = "Ge mig " + Child.name;
            audioManager = FindObjectOfType<Audio>();
            if (audioManager != null)
            {
                //In "Monkey" you write the name the sound was given in the AudioManager array
                audioManager.TriggerSound(Child.name);
            }
        }
    }

    // Checks if its the right item and either removes it if its correct and asks for next item or says that its the wrong item
    public void On_Select_Entered_Snap(SelectEnterEventArgs args)
    {
        if (GameObject.ReferenceEquals(args.interactableObject, Child.GetComponent<XRGrabInteractable>()))
        {
            //GetComponent<MeshRenderer>().material.color = Color.green;
            // TODO: update the score here;
            if (truth == false)
            {
                Child.SetActive(false);
                Socket.GetComponent<XRSocketInteractor>().socketActive = false;

                //Wait for 5 seconds.
                StartCoroutine(DelayedAction(3));
                // Request++;
            }


        }
        else
        {
            Debug.Log("Wrong Tool");
            // GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }

    // On exeting the dropzone (nothing here yet)
    public void On_Select_Exited_Snap(SelectExitEventArgs args)
    {
        // GetComponent<MeshRenderer>().material.color = Color.white;
    }

    IEnumerator DelayedAction(float delay)
    {
        Debug.Log("Action started");
        yield return new WaitForSeconds(delay);
        Debug.Log(delay + " seconds passed");

        Socket.GetComponent<XRSocketInteractor>().socketActive = true;
        Child.SetActive(true);
        ChildUpdate();
    }
}
