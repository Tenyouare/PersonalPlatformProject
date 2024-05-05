using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
    [Header("Pool Settings")]
    public List<GameObject> pooledObjects = new List<GameObject>();

    [SerializeField] int poolSize;

    [Header("Prefab Informations")]
    [SerializeField] GameObject prefab;
    [SerializeField] Vector3 platformVector;
    [SerializeField] Vector3 startPos;

    [Header("Other Settings")]

    [SerializeField] GameObject player;

    private void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            var obj = Instantiate(prefab);
            platformVector = new Vector3(0, 9, 0);
            
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }



    public GameObject GetObjectFromPool()
    {
        foreach (var obj in pooledObjects)
        {
            if (!obj.activeInHierarchy)
            {

                obj.transform.position = PrefHeight();
                obj.SetActive(true);
                return obj;

            }
        }
        /*GameObject newObj = Instantiate(prefab);
        pooledObjects.Add(newObj);
        newObj.transform.position = PrefHeight();
        return newObj;*/
        return null;

    }

    public Vector3 PrefHeight()
    {
        foreach (var obj in pooledObjects)
        {
            if (obj.activeInHierarchy)
            {
                platformVector += new Vector3(0, 3, 0);
                return platformVector;
            }

        }
        return platformVector;
    }

    public GameObject ReturnObjectToPool()
    {
        foreach(var obj in pooledObjects)
        {
            if(player.transform.position.y > obj.transform.position.y + 10)
            {
                obj.SetActive(false);
                return obj;
            }
        }
        return null;
    }



}
