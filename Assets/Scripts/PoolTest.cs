using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolTest : MonoBehaviour
{
    [SerializeField] private ObjectPool objectPool;
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private float returnInterval = 1f;
    [SerializeField] GameManager gameManagerScript;
    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();

        StartCoroutine(nameof(SpawnRoutine));
        StartCoroutine(nameof(ReturnObject));
    }


    private IEnumerator SpawnRoutine()
    {
        while (gameManagerScript.gameOver == false)
        {
            var obj = objectPool.GetObjectFromPool();

            yield return new WaitForSeconds(spawnInterval);
        }

    }

    private IEnumerator ReturnObject()
    {
        while (gameManagerScript.gameOver == false)
        {
            var obj = objectPool.ReturnObjectToPool();
            yield return new WaitForSeconds(returnInterval);
        }
    }
}
