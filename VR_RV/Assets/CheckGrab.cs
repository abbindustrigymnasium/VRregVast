using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CheckGrab : MonoBehaviour
{
    private Audio audioManager;
    private XRGrabInteractable grabInteractable;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnGrabbed);
        grabInteractable.selectExited.AddListener(OnReleased);
    }

    private void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrabbed);
        grabInteractable.selectExited.RemoveListener(OnReleased);
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        audioManager = FindObjectOfType<Audio>();
        if (audioManager != null)
        {
            //In "Monkey" you write the name the sound was given in the AudioManager array
            audioManager.TriggerSound("Sug(30s)");

        }

    }

    private void OnReleased(SelectExitEventArgs args)
    {
        audioManager = FindObjectOfType<Audio>();
        if (audioManager != null)
        {
            //In "Monkey" you write the name the sound was given in the AudioManager array
            audioManager.EndSound("Sug(30s)");

        }

    }
}
