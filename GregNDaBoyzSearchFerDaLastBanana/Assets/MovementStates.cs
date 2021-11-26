using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStates : MonoBehaviour
{
    public PlayerMovement player;

    void Start()
    {
        player = GetComponent<PlayerMovement>();
        Idle();
    }

    // States
    void Idle()
    {
      //  SetAnimationState("Idle");
        CanMoveWithSpeed(player.groundedMoveSpeed);
        if(player.rb.velocity.sqrMagnitude > 0.5f && player.isGrounded)
        {
            Walking();
            return;
        }
        Idle();
    }
    void Walking()
    {
       // SetAnimationState("Walking");
        CanMoveWithSpeed(player.groundedMoveSpeed);

        if (player.rb.velocity.sqrMagnitude < 0.5f && player.isGrounded)
        {
            Idle();
            return;
        }
        Walking();

    }
    void Running()
    {
        SetAnimationState("Running");

    }

    void Jump()
    {
        SetAnimationState("Jump");

    }
    void Falling()
    {
        SetAnimationState("Falling");

    }

    void GetOnLadder()
    {
        SetAnimationState("GetOnLadder");

    }
    void Climbing()
    {
        SetAnimationState("Climbing");

    }
    void GetOffLadder()
    {
        SetAnimationState("GetOffLadder");

    }

    void Respawning()
    {
        SetAnimationState("Respawning");
    }


    // Functions
    void SetAnimationState(string animationName)
    {
        player.animation.SetBool("Idle", false);
        player.animation.SetBool("Walking", false);
        player.animation.SetBool("Running", false);
        player.animation.SetBool("Jump", false);
        player.animation.SetBool("Falling", false);
        player.animation.SetBool("GetOnLadder", false);
        player.animation.SetBool("Climbing", false);
        player.animation.SetBool("GetOffLadder", false);
        player.animation.SetBool("Respawning", false);

        player.animation.SetBool(animationName, true);

    }

    void CanMoveWithSpeed(float speed)
    {
        Debug.Log(speed);
        player.rb.velocity = new Vector2(player.movement.x * speed, player.rb.velocity.y);
    }


}
