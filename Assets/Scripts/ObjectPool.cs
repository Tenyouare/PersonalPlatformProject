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
    [SerializeField] float firstHeight;
    [SerializeField] float xValue = 15f;

    [Header("Other Settings")]

    [SerializeField] GameObject player;

    private void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            var obj = Instantiate(prefab);
            platformVector = RandomPositionGenerate();

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
                obj.transform.localScale = ObjectScaler();
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
                platformVector += RandomPositionGenerate();
                platformVector.x = Random.Range(-xValue, xValue);

                return platformVector;
            }

        }
        return platformVector;
    }

    Vector3 ObjectScaler()
    {
        Transform objTransform = transform;

        Vector3 scale = objTransform.localScale;

        scale.x = Random.Range(5, 10);
        objTransform.localScale = scale;
        return objTransform.localScale;
    }

    public GameObject ReturnObjectToPool()
    {
        foreach (var obj in pooledObjects)
        {
            if (player.transform.position.y > obj.transform.position.y + 15)
            {
                obj.SetActive(false);
                return obj;
            }
        }
        return null;
    }

    Vector3 RandomPositionGenerate()
    {
        firstHeight = 4f;
        var randomXVector = Random.Range(-xValue, xValue);
        Vector3 firstVector = new Vector3(randomXVector, firstHeight, 0);
        return firstVector;
    }

}
