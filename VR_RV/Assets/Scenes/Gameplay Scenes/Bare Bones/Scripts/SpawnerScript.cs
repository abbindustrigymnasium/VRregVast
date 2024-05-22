using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject BloodPrefab;

    void Start()
    {
        StartCoroutine(SpawnBloodRoutine());
    }

    IEnumerator SpawnBloodRoutine()
    {
        while (true)
        {
            SpawnBlood();
            yield return new WaitForSeconds(1f);
        }
    }

    void SpawnBlood()
    {
        Instantiate(BloodPrefab, transform.position, Quaternion.identity);
    }
}
