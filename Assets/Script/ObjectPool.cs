using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    private Dictionary<GameObject, Queue<GameObject>> poolDictionary = new Dictionary<GameObject, Queue<GameObject>>();

    void Awake()
    {
        Instance = this;
    }

    public GameObject Get(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(prefab))
        {
            poolDictionary.Add(prefab, new Queue<GameObject>());
        }

        GameObject obj;

        if (poolDictionary[prefab].Count > 0)
        {
            obj = poolDictionary[prefab].Dequeue();
        }
        else
        {
            obj = Instantiate(prefab);
        }

        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.SetActive(true);

        return obj;
    }

    public void Release(GameObject prefab, GameObject obj)
    {
        obj.SetActive(false);
        poolDictionary[prefab].Enqueue(obj);
        //Debug.Log($"池子 {prefab.name} 目前數量: {poolDictionary[prefab].Count}");
    }
}
