using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : IUpdateBehavior
{
    // Variables
    EnemyBase enemy;
    bool canMove = true;
    bool hasAttacked;

    public float waitTimeForAttack;
    public float waitTimeAfterAttack;

    // Constructor
    public Patrol(EnemyBase tempEnemy, float waitTimeForAtk, float waitTimeAfterAtk)
    {
        enemy = tempEnemy;
        waitTimeForAttack = waitTimeForAtk;
        waitTimeAfterAttack = waitTimeAfterAtk;
    }

    // Function
    public void Update(GameObject gameObject)
    {
        // Setting Variables
        Transform transform = gameObject.transform;
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        Vector3 topPos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        Vector3 forwardPos = new Vector3(transform.position.x + (1 * transform.right.x), transform.position.y, transform.position.z);

        // Raycasts
        RaycastHit2D frontRay = Physics2D.Raycast(transform.position, transform.right, enemy.viewRange, GameConstants.SetLayerMask(GameConstants.LAYER_GROUND));
        RaycastHit2D topRay = Physics2D.Raycast(topPos, transform.right, enemy.viewRange, GameConstants.SetLayerMask(GameConstants.LAYER_GROUND));
        RaycastHit2D groundRay = Physics2D.Raycast(forwardPos, transform.up * -1, enemy.viewRange * 2, GameConstants.SetLayerMask(GameConstants.LAYER_GROUND));
        RaycastHit2D playerRay = Physics2D.Raycast(transform.position, transform.right, enemy.playerViewRange, GameConstants.SetLayerMask(GameConstants.LAYER_PLAYER));
        // Functionality

        if (playerRay.collider != null)
        {
            if (!hasAttacked)
            {
                canMove = false;
                rb.velocity = Vector2.zero;
                enemy.StartCoroutine(PlayerSpotted(gameObject));
            }
        }

        if (canMove == true)
        {
            // Moving
            rb.velocity = new Vector2(transform.right.x * enemy.speed, rb.velocity.y);

            // Checking Raycast
            if (groundRay.collider == null || groundRay.collider.CompareTag("Death"))
            {
                Flip(transform);
            }
            else if (frontRay.collider != null && topRay.collider != null)
            {
                Flip(transform);
            }
            else if (frontRay.collider != null && topRay.collider == null)
            {
                rb.velocity = new Vector2(rb.velocity.x, 7);
            }
            else if (frontRay.collider != null)
            {
                Flip(transform);
            }
        }
    }

    
    IEnumerator PlayerSpotted(GameObject gameObject)
    {
        hasAttacked = true;
        gameObject.GetComponent<Animator>().SetTrigger("Attack");
        yield return new WaitForSeconds(waitTimeForAttack);
        enemy.PerformAttack();
        yield return new WaitForSeconds(waitTimeAfterAttack);
        canMove = true;
        hasAttacked = false;
    }
    void Flip(Transform transform)
    {
        transform.Rotate(0, 180, 0);
    }
}
