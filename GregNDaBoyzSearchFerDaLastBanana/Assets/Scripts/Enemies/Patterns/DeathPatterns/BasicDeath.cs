using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicDeath : IDeathBehavior
{
    EnemyBase enemy;

    public BasicDeath(EnemyBase tempEnemy)
    {
        enemy = tempEnemy;
    }

    public void Die()
    {
        // Setting Variables
        GameObject gameObject = enemy.gameObject;
        Transform transform = gameObject.transform;

        enemy.rb.velocity = Vector2.zero;
        enemy.rb.isKinematic = true;

        enemy.SetAttackBehaviour(new NoAttack());
        enemy.SetUpdateBehavior(new NoUpdate());
        enemy.SetAnimatorBehavior(new NoAnimations());

        enemy.GetComponent<Collider2D>().enabled = false;

        enemy.animator.SetTrigger("die");
        foreach (GameObject effect in enemy.deathEffects)
        {
            GameObject.Instantiate(effect, transform.position, Quaternion.identity);
        }
        Object.Destroy(gameObject, 1f);
    }
}
