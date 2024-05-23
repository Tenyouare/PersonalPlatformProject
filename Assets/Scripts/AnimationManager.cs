using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{

    public CharacterMovement characterMovementScript;
    private float inputHolder;
    private Vector3 velocityHolder;
    private Vector3 assetPos;
    private Vector3 offset = new Vector3(0,5,0);
    private Animator anim;
    private enum MovementState { idle, running, jumping, floating }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        characterMovementScript = GameObject.Find("Player").GetComponent<CharacterMovement>();
        
    }

    // Update is called once per frame
    void Update()
    {
        inputHolder = characterMovementScript.horizontalInput;
        velocityHolder = characterMovementScript.playerRb.velocity;
        AnimationGenerator();
        //PositionEqualizer();
    }

    private void LateUpdate()
    {
        PositionEqualizer();
    }

    private void AnimationGenerator()
    {
        MovementState state;

        if (inputHolder==0 && velocityHolder.y == 0)
        {
            state = MovementState.idle;
            //anim.SetInteger("state" , (int)state);
        }
        else if(inputHolder != 0 && characterMovementScript.isOnGround ==true)
        {
            state = MovementState.running;
            //anim.SetInteger("state", (int)state);
        }
        else
        {
            state = MovementState.idle;
        }

        if (velocityHolder.y > 0.1)
        {
            state = MovementState.jumping;
            //anim.SetInteger("state", (int)state);
        }
        else if(velocityHolder.y < -0.1)
        {
            state = MovementState.floating;
            //anim.SetInteger("state", (int)state);
        }
        
        anim.SetInteger("state", (int)state);
    }

    void PositionEqualizer()
    {
        assetPos = transform.position;
        assetPos.x = characterMovementScript.playerPos.x;
        assetPos.y = characterMovementScript.playerPos.y + offset.y;
        assetPos.z = characterMovementScript.playerPos.z;
    }
}
