using UnityEngine;

public class Patrol : IUpdateBehavior
{
    // Variables
    EnemyBase enemy;

    // Constructor
    public Patrol(EnemyBase tempEnemy)
    {
        enemy = tempEnemy;
    }

    // Function
    public void Update(GameObject gameObject)
    {
        // Setting Variables
        Transform transform = gameObject.transform;
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        Vector3 topPos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);

            // Raycasts
        RaycastHit2D frontRay = Physics2D.Raycast
            (
            transform.position, transform.right,
            enemy.viewRange,
            LayerMask.GetMask(LayerMask.LayerToName(GameConstants.LAYER_GROUND))
            );
        RaycastHit2D topRay = Physics2D.Raycast
            (
            new Vector3(transform.position.x, transform.position.y + 1, transform.position.z),
            transform.right, enemy.viewRange,
            LayerMask.GetMask(LayerMask.LayerToName(GameConstants.LAYER_GROUND))
            );

        // Functionality

            // Moving
        rb.velocity = new Vector2(transform.right.x * enemy.speed, rb.velocity.y);

            // Checking Raycast
        if (frontRay.collider != null && topRay.collider != null)
        {
            Flip(transform);
        }
        else if (frontRay.collider != null && topRay.collider == null)
        {
            rb.velocity = new Vector2(rb.velocity.x, 7);
        }
        else if(frontRay.collider != null)
        {
            Flip(transform);
        }

        DrawGizmos(transform);
    }

    void Flip(Transform transform)
    {
        transform.Rotate(0, 180, 0);
    }

    void DrawGizmos(Transform transform)
    {
        // Setting Variables
        Vector3 topPos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);

        Gizmos.DrawLine(topPos, new Vector3(topPos.x + (1 * transform.right.x), topPos.y, topPos.z));
    }
}
