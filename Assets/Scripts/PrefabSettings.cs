using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSettings : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] GameObject player;
    private BoxCollider prefabBoxCol;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        prefabBoxCol = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > player.transform.position.y +.49f)
        {
            prefabBoxCol.isTrigger = true;
        }
        else if (transform.position.y < player.transform.position.y)
        {
            prefabBoxCol.isTrigger = false;
        }
    }
}
