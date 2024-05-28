using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScissorScript : MonoBehaviour
{
    public float activation;
    public bool lerpTypeTime = true;
    public float cycleTime = 0.15f;
    public bool reset = true;
    // Should be configured for each object individually
    public bool HasBaseChild;
    public Vector3 deviation = new Vector3(-1.5f, 0f, 0f);
    // Neutral object rotation vector
    public Vector3 neutralVector = new Vector3(-90f, 0f, 0f);
    // Private variables
    private float time;
    private GameObject leftChild;
    private GameObject rightChild;
    private GameObject baseChild;
    void Start()
    {
        leftChild = transform.Find("LeftBlade").gameObject;
        rightChild = transform.Find("RightBlade").gameObject;
        if (HasBaseChild) baseChild = transform.Find("Base").gameObject;
    }

    void Update()
    {
        time += Time.deltaTime;

        // Rotation vectors 
        Vector3 rotLeft = lerpTypeTime ? LerpTime(deviation, time) : LerpNormalized(deviation, activation);
        Vector3 rotRight = lerpTypeTime ? LerpTime(-deviation, time) : LerpNormalized(-deviation, activation);

        // Left blade
        Quaternion combinedRotationLeft = Quaternion.Euler(neutralVector) * Quaternion.Euler(rotLeft);
        leftChild.transform.rotation = combinedRotationLeft;
        
        // Right blade
        Quaternion combinedRotationRight = Quaternion.Euler(neutralVector) * Quaternion.Euler(rotRight);
        rightChild.transform.rotation = combinedRotationRight;

        // Base
        if (HasBaseChild)
        {
            baseChild.transform.rotation = Quaternion.Euler(neutralVector);
        }

        if (reset)
        {
            time = 0;
            reset = false;
        }
    }

    // Interpolate with established time system
    Vector3 LerpTime(Vector3 deviation, float time)
    {
        return time < 2*cycleTime
               ? deviation * ((cycleTime - Mathf.Abs(time - cycleTime)) / cycleTime)
               : new Vector3(0, 0, 0);
    }

    // Regular interpolation (0 <= lerp <= 1)
    Vector3 LerpNormalized(Vector3 deviation, float lerp)
    {
        return 0 <= lerp && lerp <= 1
               ? deviation * lerp
               : new Vector3(0, 0, 0);
    }
}
