using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject platformPrefab;
    [SerializeField] bool  camLock;
    private Vector3 offset;

    [SerializeField] GameObject player;
    [SerializeField] Vector3 platformVector;

    private Vector3 _currentVel = Vector3.zero;
    [SerializeField] float smoothTime = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        player = GameObject.Find("Player");


        offset = new Vector3(0, 0.25f, -15f);
        platformVector = new Vector3(0, 9, 0);
        StartCoroutine(RepeatGenerateFunction());
    }

    // Update is called once per frame
    void Update()
    {
        if (camLock== false) 
        {
            Debug.Log("false");
        }
        else if (camLock== true) 
        { 
            Debug.Log("true"); 
        }

    }

    private void LateUpdate()
    {
        if (camLock == false)
        {
            mainCamera.transform.Translate(Vector3.up * 0.5f * Time.deltaTime);
        }
        CameraSetting();
    }

    void CameraSetting()
    {
        if (player.transform.position.y > mainCamera.transform.position.y)
        {
            camLock = true;
            Vector3 _cameraPos = mainCamera.transform.position;
            Vector3 _newPos = player.transform.position + offset;
            Vector3 _targetPosition = new Vector3(_cameraPos.x, _newPos.y, _cameraPos.z);

            //Old camera follow code, does not work properly
            //mainCamera.transform.position = new Vector3(_cameraPos.x, _newPos.y, _cameraPos.z);

            //new camera follow code, works more smoothly
            mainCamera.transform.position = Vector3.SmoothDamp(_cameraPos, _targetPosition,
                ref _currentVel, smoothTime);

        }
        else
        {
            camLock = false;
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
    }
}
