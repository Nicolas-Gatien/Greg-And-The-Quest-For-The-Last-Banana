using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_BasicJump : IOnSpacePressedBehavior
{
    public float jumpHeight;
    public float checkRadius;
    public Transform checkPos;
    public LayerMask whatIsGround;
    bool isGrounded;

    public float coyoteTime;
    float curCoyoteTime;
    public float jumpTime;
    float curJumpTime;

    public void SpaceUpdate() 
    {
        isGrounded = Physics2D.OverlapCircle(checkPos.position, checkRadius, whatIsGround);

        curCoyoteTime -= Time.deltaTime;
        curJumpTime -= Time.deltaTime;
    }
    public void SpacePressed()
    {

    }
}
