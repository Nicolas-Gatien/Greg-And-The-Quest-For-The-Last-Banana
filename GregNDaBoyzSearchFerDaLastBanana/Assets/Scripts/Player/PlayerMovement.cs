using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;

    public float speed;
    Vector2 movement;

    public float jumpHeight;
    public float checkRadius;
    public Transform checkPos;
    public LayerMask whatIsGround;
    bool isGrounded;

    public Animator anim;

    [Space]
    public float climbSpeed;
    public LayerMask whatIsLadder;
    Vector2 ladderMove;
    bool onLadder;

    private float gScale;

    public float coyoteTime;
    float curCoyoteTime;
    public float jumpTime;
    float curJumpTime;

    public GameObject deathAnimation;
    public Vector2 currentCheckpoint;

    public bool canMove;
    public float respawnTime = 1;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        gScale = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        onLadder = Physics2D.OverlapCircle(transform.position, checkRadius, whatIsLadder);
        isGrounded = Physics2D.OverlapCircle(checkPos.position, checkRadius, whatIsGround);
        movement.x = Input.GetAxis("Horizontal");
        ladderMove.y = Input.GetAxis("Vertical");

        curCoyoteTime -= Time.deltaTime;
        curJumpTime -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            curJumpTime = jumpTime;
        }

        if (onLadder)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.gravityScale = 0;
            isGrounded = true;
        }else
        {
            rb.gravityScale = gScale;
        }

        if (onLadder && ladderMove.y != 0)
        {
            anim.SetBool("IsClimbing", true);
        }
        else
        {
            anim.SetBool("IsClimbing", false);
        }

        if (isGrounded)
        {
            curCoyoteTime = coyoteTime;
            anim.SetBool("IsGrounded", true);
        }else
        {
            anim.SetBool("IsGrounded", false);
        }
        if (onLadder && ladderMove.y != 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, climbSpeed * ladderMove.y);
        }

        if (curJumpTime >= 0 && curCoyoteTime >= 0 && canMove)
        {
            anim.SetTrigger("Jump");

            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            curCoyoteTime = -1;
            curJumpTime = -1;

        }
        if(canMove)
        {
            rb.velocity = new Vector2(movement.x * speed, rb.velocity.y);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
        }else if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
        }

        if (rb.velocity.sqrMagnitude > 0.1)
        {
            anim.SetBool("IsWalking", true);
        }else
        {
            anim.SetBool("IsWalking", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Death"))
        {
            Instantiate(deathAnimation, transform.position, Quaternion.identity);
            StartCoroutine(Respawn());
        }
    }

    IEnumerator Respawn()
    {
        rb.velocity = Vector2.zero;
        canMove = false;
        transform.position = currentCheckpoint;
        anim.SetTrigger("respawn");
        yield return new WaitForSeconds(respawnTime);
        canMove = true;
    }
}
