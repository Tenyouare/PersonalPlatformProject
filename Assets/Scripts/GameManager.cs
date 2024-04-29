using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject platformPrefab;
    [SerializeField] BoxCollider platformPrefabBox;
    [SerializeField] List<GameObject> platformList;
    private Vector3 offset;


    private CharacterMovement CharacterMovementScript;
    [SerializeField] GameObject player;
    Vector3 platformVector;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        player = GameObject.Find("Player");


        offset = new Vector3(0, 0.25f, -15f);
        CharacterMovementScript = GameObject.Find("Player").GetComponent<CharacterMovement>();
        platformVector = new Vector3(0, 9, 0);
        StartCoroutine(RepeatGenerateFunction());
    }

    // Update is called once per frame
    void Update()
    {
        
        mainCamera.transform.Translate(Vector3.up * 0.3f * Time.deltaTime);
        CalculateList();
    }
    private void LateUpdate()
    {
        CameraSetting();
    }

    void CameraSetting()
    {
        if (player.transform.position.y > mainCamera.transform.position.y)
        {
            Vector3 _cameraPos = mainCamera.transform.position;
            Vector3 _newPos = player.transform.position + offset;
            mainCamera.transform.position = new Vector3(_cameraPos.x, _newPos.y, _cameraPos.z);
            //mainCamera.transform.position = player.transform.position + offset;
        }
    }
    IEnumerator RepeatGenerateFunction()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            PrefabGenerate();
        }
    }

    void PrefabGenerate()
    {
        Instantiate(platformPrefab, platformVector, platformPrefab.transform.rotation);
        platformVector += new Vector3(0, 3, 0);
        platformPrefab.transform.position = platformVector;
        platformList.Add(platformPrefab);
    }

    void CalculateList()
    {
        foreach (GameObject platformPrefab in platformList)
        {
            platformPrefabBox = platformPrefab.GetComponent<BoxCollider>();
            if (platformPrefab.transform.position.y > player.transform.position.y)
            {
                platformPrefabBox.isTrigger = true;

            }
            else if (platformPrefab.transform.position.y <= transform.position.y + 0.5f)
            {
                platformPrefabBox.isTrigger = false;
            }
        }
    }


}
