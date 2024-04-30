using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace VRregVast.StandardLibrary
{
    public static class SceneManagement
    {
        public static void New_Scene(object scene_identifier = null, GameObject user = null, Transform target = null)
        {
            // Loads a new scene.
            // Sets the player's transform to another transform if user and target are passed.
            //
            // scene_identifier can be string scene_name or int build_index.
            // If no scene_identifier is passed, the next scene in the build is used.
            //
            // user is a GameObject, likely the XR Rig.
            // target is a Transform in the next scene 
            // (or a DontDestroyOnLoad GameObject's Transform in the current scene).
            // If user or target are not passed, no transformation occurs

            if (scene_identifier is string scene_name)
            {
                SceneManager.LoadScene(scene_name);
            }
            else if (scene_identifier is int build_index)
            {
                SceneManager.LoadScene(build_index);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }

            if (user != null && target != null)
            {
                user.transform.position = target.position;
                user.transform.rotation = target.rotation;
            }
        }
    }
}
