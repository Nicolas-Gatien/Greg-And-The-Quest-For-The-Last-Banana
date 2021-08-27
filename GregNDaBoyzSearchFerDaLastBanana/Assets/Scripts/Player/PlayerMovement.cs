using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Variables
    [Header("Stats")]
    public float baseMoveSpeed;
    public float baseLadderClimbSpeed;
    public int numOfJumps;
    public float jumpHeight;

    [Header("LayerMasks")]
    public LayerMask whatIsGround;
    public LayerMask whatIsLadder;

    [Header("VFX")]
    public GameObject deathAnimation;

    [Header("Polish")]
    public float coyoteTime;
    float curCoyoteTime;
    public float jumpTime;
    float curJumpTime;

    [Header("Hidden Variables")]
    Vector2 movement;
    Vector2 ladderMove;
    bool isGrounded;
    bool onLadder;
    float gScale;

    [Header("Components")]
    Rigidbody2D rb;
    Animator anim;

    [Header("States")]
    public bool canMove;
    public GregStates states;

    [Header("Functional Variables")]
    public Transform groundCheck;
    public float checkRadius;
    [HideInInspector] public Vector2 currentCheckpoint;
    public float respawnTime = 1.3f;

    // Stored Variables
    rbComponent storedRigidbody;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        whatIsGround = GameConstants.SetLayerMask(GameConstants.LAYER_GROUND);
        whatIsLadder = GameConstants.SetLayerMask(GameConstants.LAYER_LADDER);
        #region Stored Rigidbody
        storedRigidbody.angularDrag = rb.angularDrag;
        storedRigidbody.angularVelocity = rb.angularVelocity;
        storedRigidbody.drag = rb.drag;
        storedRigidbody.gravityScale = rb.gravityScale;
        storedRigidbody.inertia = rb.inertia;
        storedRigidbody.mass = rb.mass;
        storedRigidbody.rotation = rb.rotation;

        storedRigidbody.name = rb.name;
        storedRigidbody.tag = rb.tag;

        storedRigidbody.attachedColliderCount = rb.attachedColliderCount;

        storedRigidbody.fixedAngle = rb.fixedAngle;
        storedRigidbody.freezeRotation = rb.freezeRotation;
        storedRigidbody.isKinematic = rb.isKinematic;
        storedRigidbody.simulated = rb.simulated;
        storedRigidbody.useAutoMass = rb.useAutoMass;
        storedRigidbody.useFullKinematicContacts = rb.useFullKinematicContacts;

        storedRigidbody.bodyType = rb.bodyType;
        storedRigidbody.sleepMode = rb.sleepMode;
        storedRigidbody.constraints = rb.constraints;
        storedRigidbody.interpolation = rb.interpolation;
        storedRigidbody.collisionDetectionMode = rb.collisionDetectionMode;
        storedRigidbody.hideFlags = rb.hideFlags;

        storedRigidbody.centerOfMass = rb.centerOfMass;
        storedRigidbody.position = rb.position;
        storedRigidbody.velocity = rb.velocity;
        storedRigidbody.worldCenterOfMass = rb.worldCenterOfMass;
        storedRigidbody.transform = rb.transform;
        storedRigidbody.gameObject = rb.gameObject;
        storedRigidbody.sharedMaterial = rb.sharedMaterial;
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        onLadder = Physics2D.OverlapCircle(transform.position, checkRadius, whatIsLadder);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        movement.x = Input.GetAxis("Horizontal");
        ladderMove.y = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            curJumpTime = jumpTime;
        }

        curCoyoteTime -= Time.deltaTime;
        curJumpTime -= Time.deltaTime;

        if (onLadder) { states = GregStates.Climbing; } // if on ladder
        if (isGrounded && !onLadder) { states = GregStates.Grounded; } // is Grounded
        if (!isGrounded && !onLadder) { states = GregStates.Falling; } // if falling

        if (states == GregStates.Climbing)
        {
            ClimbingBehavior();
        }
        if (states == GregStates.Grounded)
        {
            GroundedBehavior();
        }
        if (states == GregStates.Falling)
        {
            FallingBehavior();
        }

        anim.SetFloat("velocity", rb.velocity.sqrMagnitude);
        #region Old Code
        /*if (onLadder)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.gravityScale = 0;
            isGrounded = true;
        }
        else
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
        }
        else
        {
            anim.SetBool("IsGrounded", false);
        }
        if (onLadder && ladderMove.y != 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, baseLadderClimbSpeed * ladderMove.y);
        }

        if (curJumpTime >= 0 && curCoyoteTime >= 0 && canMove)
        {
            anim.SetTrigger("Jump");

            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            curCoyoteTime = -1;
            curJumpTime = -1;

        }
        if (canMove)
        {
            rb.velocity = new Vector2(movement.x * baseMoveSpeed, rb.velocity.y);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
        }

        if (rb.velocity.sqrMagnitude > 0.1)
        {
            anim.SetBool("IsWalking", true);
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }*/
        #endregion
    }

    void ClimbingBehavior()
    {

    }
    void GroundedBehavior()
    {
        rb.velocity = new Vector2(movement.x * baseMoveSpeed, rb.velocity.y);
        if(movement.x > 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }else if(movement.x < 0)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }
    void FallingBehavior()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Death"))
        {
            Instantiate(deathAnimation, transform.position, Quaternion.identity);
            StartCoroutine(Respawn());
        }
    }

    // Coroutines
    IEnumerator Respawn()
    {
        rb.velocity = Vector2.zero;
        canMove = false;
        transform.position = currentCheckpoint;
        anim.SetTrigger("respawn");
        yield return new WaitForSeconds(respawnTime);
        canMove = true;
    }

    // Functions
    void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
    }
}

public struct rbComponent
{
    public float angularDrag;
    public float angularVelocity;
    public float drag;
    public float gravityScale;
    public float inertia;
    public float mass;
    public float rotation;

    public string name;
    public string tag;

    public int attachedColliderCount;

    public bool fixedAngle;
    public bool freezeRotation;
    public bool isKinematic;
    public bool simulated;
    public bool useAutoMass;
    public bool useFullKinematicContacts;

    public RigidbodyType2D bodyType;
    public RigidbodySleepMode2D sleepMode;
    public RigidbodyConstraints2D constraints;
    public RigidbodyInterpolation2D interpolation;
    public CollisionDetectionMode2D collisionDetectionMode;
    public HideFlags hideFlags;

    public Vector2 centerOfMass;
    public Vector2 position;
    public Vector2 velocity;
    public Vector2 worldCenterOfMass;
    public Transform transform;
    public GameObject gameObject;
    public PhysicsMaterial2D sharedMaterial;
}