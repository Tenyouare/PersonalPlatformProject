using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaMovement : MonoBehaviour
{
    [SerializeField] GameManager gameManagerScript;

    [SerializeField] GameObject lava;
    private Rigidbody lavaRb;
    private Vector3 lavaPos;
    private float xFinishPos = 30f;
    private Vector3 lavaVelocity;
    
    private float speed = 3f;
    private float takeOff = 3f;
    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();

        lavaRb = GetComponent<Rigidbody>();
        lavaVelocity = new Vector3(speed, takeOff, lavaRb.velocity.z);
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
        LavaPositionCheck();
    }

    void LavaPositionCheck()
    {
        if (gameManagerScript.gameOver == false)
        {
            lavaRb.velocity = lavaVelocity;
            lavaPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

            if (lavaPos.x >= xFinishPos)
            {
                transform.position = new Vector3(-xFinishPos, lavaPos.y, lavaPos.z);
                Debug.Log("e hadi artýk");
            }
        }
        else
        {
            lavaRb.velocity = Vector3.zero;
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameManagerScript.gameOver = true;
            Debug.Log("Temas var");
        }
    }
}
