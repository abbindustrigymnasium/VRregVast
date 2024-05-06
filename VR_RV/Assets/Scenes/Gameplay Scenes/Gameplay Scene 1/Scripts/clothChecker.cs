using UnityEngine;

public class ColliderCollisionChecker : MonoBehaviour
{

    public turnOn turnOnScript;
    public lockCloth turnOnScript2;

    public bool debugLog = true;

    private void OnTriggerEnter(Collider other)
    {
        if (debugLog)
        {
            Debug.Log("Colliders have collided!");
        }

        turnOnScript.bollean = true;
        turnOnScript2.freezeRigidbody = true;
    }
}
