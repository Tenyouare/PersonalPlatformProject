using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject player;
    [SerializeField] GameObject asset;
    float horizontalInput;


    private Vector3 playerPos;
    private Vector3 leftPlayerBound;
    private Vector3 rightPlayerBound;
    private float playerBoundX = 16.4f;

    private Rigidbody playerRb;

    private float gravityModifier = 2.5f;
    [SerializeField] float speed = 10f;
    [SerializeField] float jumpForce = 20f;
    [SerializeField] private bool isOnGround = true;


    void Start()
    {
        Physics.gravity *= gravityModifier;
        playerRb = GetComponent<Rigidbody>();
    }


    void Update()
    { 
        horizontalInput = Input.GetAxis("Horizontal");

        //move by adding velocity
        playerRb.velocity = new Vector3 (horizontalInput * speed, playerRb.velocity.y, playerRb.velocity.z);

        //move by adding force
        //playerRb.AddForce(Vector3.right * speed * horizontalInput);

        PlayerJumpCheck();
        PlayerTeleport();
        RotateAsset();
    }

    void PlayerJumpCheck()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            //jump by adding velocity 
            playerRb.velocity = new Vector3(playerRb.velocity.x, jumpForce, playerRb.velocity.z);
            
            // jump by adding force
            //playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Ground") || collision.collider.gameObject.CompareTag("Platform"))
        {
            isOnGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Ground") || collision.collider.gameObject.CompareTag("Platform"))
        {
            isOnGround = false;
        }
    }

    void PlayerTeleport()
    {
        playerPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        leftPlayerBound = new Vector3(-playerBoundX, transform.position.y, transform.position.z);
        rightPlayerBound = new Vector3(playerBoundX, transform.position.y, transform.position.z);

        if (playerPos.x < leftPlayerBound.x)
        {
            player.transform.position = rightPlayerBound;
        }
        else if (playerPos.x > rightPlayerBound.x)
        {
            player.transform.position = leftPlayerBound;
        }
    }

    void RotateAsset()
    {
        Quaternion currentRotation = asset.transform.rotation;
        if (VelocityCatcher(horizontalInput) > 0)
        {
            Quaternion rotation = Quaternion.Euler(0, 90, 0);
            asset.transform.rotation = rotation;
        }
        else if(VelocityCatcher(horizontalInput)<0)
        {
            Quaternion rotation = Quaternion.Euler(0,270, 0);
            asset.transform.rotation = rotation;
        }
    }

    float VelocityCatcher(float velocity)
    {
        float velocityCatcher = velocity;
        return velocityCatcher;
    }
}
