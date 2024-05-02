using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolTest : MonoBehaviour
{
    [SerializeField] private ObjectPool objectPool;
    [SerializeField] private float spawnInterval = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(nameof(SpawnRoutine));
    }


    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            var obj = objectPool.GetObjectFromPool();

            yield return new WaitForSeconds(spawnInterval);
        }

    }
}
