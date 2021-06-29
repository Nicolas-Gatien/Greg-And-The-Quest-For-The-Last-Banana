using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedEnemy : MonoBehaviour, IAnimatorBehavior
{
    bool isGrounded;

    // Function
    public void Animate(Animator anim, GameObject gameObject)
    {
        // Setting Variables
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        Transform transform = gameObject.transform;
        Vector2 bottom = new Vector2(transform.position.x, transform.position.y - (transform.localScale.y/2));
        isGrounded = Physics2D.OverlapCircle(bottom, 0.3f, GameConstants.SetLayerMask(GameConstants.LAYER_GROUND));

        // Functionality
        if(rb.velocity.sqrMagnitude > 0.1)
        {
            anim.SetBool("isMoving", true);
            Debug.Log("Walking");
        }else
        {
            anim.SetBool("isMoving", false);
        }

        if (isGrounded)
        {
            anim.SetBool("isGrounded", true);
        }else
        {
            anim.SetBool("isGrounded", false);
        }
        Debug.Log(gameObject + " " + isGrounded);

     //   GameObject groundcheck = Instantiate(new GameObject("Groundcheck"), bottom, Quaternion.identity);
     //   groundcheck.transform.parent = transform;
    }
}
