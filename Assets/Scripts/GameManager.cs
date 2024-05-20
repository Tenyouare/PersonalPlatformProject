using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject platformPrefab;
    [SerializeField] bool  camLock;
    public TextMeshProUGUI scoreText;
    private int scoreHolder = 0;
    private Vector3 camOffset;

    [SerializeField] GameObject player;

    private int score = 0;
    private Vector3 _currentVel = Vector3.zero;
    [SerializeField] float smoothTime = 0.25f;

    void Start()
    {
        mainCamera = Camera.main;
        player = GameObject.Find("Player");

        camOffset = new Vector3(0, 0.25f, -15f);
        scoreText.text = "Score : " + score;

    }


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
        ScoreCalculator();

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
        if (player.transform.position.y > mainCamera.transform.position.y+2.5f)
        {
            camLock = true;
            Vector3 _cameraPos = mainCamera.transform.position;
            Vector3 _newPos = player.transform.position + camOffset;
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

    void ScoreCalculator()
    {
        Vector3 instantPlayerPos = player.transform.position;
        score = Mathf.RoundToInt(instantPlayerPos.y / 4);
        
        if (scoreHolder + 1 == score ) 
        {
            scoreText.text = "Score : " + score.ToString();
            scoreHolder++;
            Debug.Log(scoreHolder); 
        }

        /*if(score > scoreHolder)
        {
            scoreText.text = "Score : " + score.ToString();
        }*/
    }

}
