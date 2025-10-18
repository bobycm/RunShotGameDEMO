using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnSystem : MonoBehaviour
{
    public GameObject testOBJ;
    
    void Start()
    {
        InvokeRepeating("SpawnObject", 0.0f, 1.0f);
    }


    void SpawnObject()
    {
        float randomX = Random.Range(-3, 3);
        Vector3 spawnPosition = new Vector3(
            randomX,
            transform.position.y, 
            transform.position.z
        );
        Instantiate(testOBJ, spawnPosition, transform.rotation);
    }
}
