using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleTest : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    private Vector3 objPos;
    private Vector3 objScale;
    private float spawnValue = 14.0f;
    private float minObjWidth = 3f;
    private float maxObjWidth = 5f;

    [SerializeField] private float spawnInterval = 5f;

    // Start is called before the first frame update

    private void Awake()
    {
        objScale = transform.localScale;
        objPos = transform.position;
    }
    void Start()
    {
        //StartCoroutine(nameof(CreateObject));

        InvokeRepeating("DoSomething", 2, 2);
    }


    private IEnumerator CreateObject()
    {
        while (true)
        {
            DoSomething();


            yield return new WaitForSeconds(spawnInterval);
        }
    }
    void DoSomething()
    {
        //Vector3 falanFilan = ScaleObject();
        //objScale.x = falanFilan.x;
        PrefabScaler();
        Instantiate(prefab, PlaceObject(), prefab.transform.rotation);
    }

    Vector3 PlaceObject()
    {
        objPos.x = Random.Range(-spawnValue, spawnValue);
        Vector3 spawnPosition = new Vector3(objPos.x, objPos.y,objPos.z);
        return spawnPosition;
    }

    Vector3 ScaleObject()
    {
        var randomXScale = Random.Range(minObjWidth, maxObjWidth);
        //var xWidth = Random.Range(minObjWidth, maxObjWidth);
        Vector3 spawnWidth = new Vector3 (randomXScale, objScale.y, objScale.z);
        
        return spawnWidth;
    }

    void PrefabScaler()
    {
        Transform prefabTransform = transform;

        Vector3 scale = prefabTransform.localScale;

        scale.x = Random.Range (minObjWidth, maxObjWidth);
        prefabTransform.localScale = scale;
    }
}
