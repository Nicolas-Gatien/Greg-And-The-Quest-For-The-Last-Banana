using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leap : IAttackBehavior
{
    EnemyBase enemy;

    public Leap(EnemyBase tempEnemy)
    {
        enemy = tempEnemy;
    }

    public void Attack(GameObject gameObject)
    {
        Transform transform = gameObject.transform;
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity += new Vector2(((Vector2.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position)) * transform.right.x) * 2f, 15);
    }
}

public class KoboldDeath : IDeathBehavior
{
    public void Die()
    {
        Debug.Log("REEEEEEEEEEEEEEEE, I've died");
    }
}

public class KoboldDamage : ITakeDamageBehavior
{
    public void TakeDamage()
    {
        Debug.Log("Ouchie");
    }
}
