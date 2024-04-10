using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class WireSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject partPrefab, parentObject;

    [SerializeField]
    [Range(1, 1000)] public int length = 1;
    public float2 dims = new float2(1.0f,1.0f);

    [SerializeField]
    float partDistance = 0.21f;

    [SerializeField]
    bool reset, spawnRope, spawnCloth, snapFirst, snapLast;

    // Update is called once per frame
    void Update()
    {
        if(reset){
            foreach (GameObject tmp in GameObject.FindGameObjectsWithTag("Player"))
            {
                Destroy(tmp);
            }
            reset = false;
        }

        if (spawnRope){
            SpawnRope();

            spawnRope = false;
        }

        if (spawnCloth){
            SpawnCloth();

            spawnCloth = false;
        }
    }

    GameObject CreateChild(Vector3 offset)
    {
        GameObject tmp;
        tmp = Instantiate(partPrefab, transform.position + offset, Quaternion.identity, parentObject.transform);
        tmp.transform.eulerAngles = new Vector3(180, 0,0);
        tmp.name = parentObject.transform.childCount.ToString();

        return tmp;
    }

void Bind(GameObject child, bool isFirstPartInSequence, params GameObject[] connectedObjects)
{
    if (isFirstPartInSequence)
    {
        Destroy(child.GetComponent<CharacterJoint>());
        if (snapFirst)
        {
            child.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }
    else
    {
        CharacterJoint[] joints = child.GetComponents<CharacterJoint>();
        for (int i = 0; i < joints.Length && i < connectedObjects.Length; i++)
        {
            joints[i].connectedBody = connectedObjects[i].GetComponent<Rigidbody>();
        }
    }
}


public void SpawnCloth()
{
    int2 count = (int2)(dims / partDistance);
    GameObject[,] childs = new GameObject[count.x, count.y];

    for (int x = 0; x < count.x; x++)
    {
        for (int y = 0; y < count.y; y++)
        {
            Vector3 offset = new Vector3(partDistance * (x + 1), partDistance * (y + 1) + 1, 0);
            GameObject child = CreateChild(offset);
            childs[x, y] = child;

            bool isFirstPartInSequence = y == 0;

            if (!isFirstPartInSequence)
            {
                if (y != 0 && x != 0)
                {
                    Bind(child, false, childs[x, y - 1], childs[x - 1, y]);
                }
            }
        }

        if(snapLast){
            parentObject.transform.Find((parentObject.transform.childCount).ToString()).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}


    public void SpawnRope(){
        // int count = (int)(length / partDistance);

        // for (int y = 0; y < count; y++){

        //     Vector3 offset = new Vector3(0, partDistance * (y + 1), 0);
        //     GameObject tmp = CreateChild(offset);

        //     bool isFirstPartInSequence = y == 0;

        //     Bind(ref tmp, isFirstPartInSequence, parentObject.transform.childCount - 1);
        // }

        // if(snapLast){
        //     parentObject.transform.Find((parentObject.transform.childCount).ToString()).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        // }
    }
}
