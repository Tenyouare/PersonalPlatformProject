using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PrefabSettings : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] GameObject player;
    private BoxCollider prefabBoxCol;

    private Vector3 prefabPos;
    private Vector3 playerPos;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        prefabBoxCol = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        prefabPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        playerPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        YPosCheck();
        FallingPlatform(playerPos, prefabPos);
    }

    void YPosCheck()
    {
        if (prefabPos.y + 1f < playerPos.y)
        {
            prefabBoxCol.isTrigger = false;
        }
        else
        {
            prefabBoxCol.isTrigger = true;
        }

        /*if (prefabPos.y > playerPos.y)
        {
            prefabBoxCol.isTrigger = true;
        }
        else if (prefabPos.y < playerPos.y)
        {
            prefabBoxCol.isTrigger = false;
        }*/
    }



    void FallingPlatform(Vector3 examplePlayerPos, Vector3 examplePrefabPos)
    {
        Rigidbody prefabRb = prefab.GetComponent<Rigidbody>();
        if (examplePlayerPos.y > examplePrefabPos.y+8)
        {
            prefabRb.isKinematic = false;
        }
        else
        {
            prefabRb.isKinematic = true;
        }
    }





    

}
