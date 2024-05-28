using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public Transform head;
    public float spawnDistance = 2;
    public GameObject menu;
    public InputActionProperty showButton;
    public GameObject ray_interaction_right;
    public GameObject ray_interaction_left;


    void Update()
    {
        if (showButton.action.WasPressedThisFrame())
        {
            // Goes through all child objects in menu and resets what window is open.
            foreach (Transform child in menu.transform)
            {
                if (child.name == "Pause")
                {
                    child.gameObject.SetActive(true);
                }
                else
                {
                    child.gameObject.SetActive(false);
                }
            }
            // Toogles the menu on or off.
            menu.SetActive(!menu.activeSelf);

            Toggle_Ray();

            // Moves the menu forward in the direction the player is looking at with the spawnDistance.
            menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
        }

        if (menu.activeSelf)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        // Makes the menu rotate to the player.
        menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z));
        // Fixes the menu so it won't be mirored.
        menu.transform.forward *= -1;
    }

    // Starts the new scene
    public void Start_Scene(string scene)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(scene);
    }

    public void Toggle_Ray()
    {
        // Turns on or off the rays from the hands.
        foreach (Transform ray_right in ray_interaction_right.transform)
        {
            if (ray_right.name == "Ray Interactor" || ray_right.name == "Ray Stabilizer")
            {
                ray_right.gameObject.SetActive(!ray_right.gameObject.activeSelf);
            }
        }

        foreach (Transform ray_left in ray_interaction_left.transform)
        {
            if (ray_left.name == "Ray Interactor" || ray_left.name == "Ray Stabilizer")
            {
                ray_left.gameObject.SetActive(!ray_left.gameObject.activeSelf);
            }
        }
    }
}
