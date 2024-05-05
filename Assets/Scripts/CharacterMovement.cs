using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject player;
    float horizontalInput;
    public List<GameObject> platforms = new List<GameObject>();

    private Vector3 playerPos;
    private Vector3 leftPlayerBound;
    private Vector3 rightPlayerBound;
    private float playerBoundX = 16.4f;

    Rigidbody playerRb;

    [SerializeField] float speed = 2f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] private bool isOnGround = true;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        horizontalInput = Input.GetAxis("Horizontal");
        playerRb.AddForce(Vector3.right * speed * horizontalInput);

        PlayerJumpCheck();
        PlayerTeleport();
    }

    void PlayerJumpCheck()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
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

}
