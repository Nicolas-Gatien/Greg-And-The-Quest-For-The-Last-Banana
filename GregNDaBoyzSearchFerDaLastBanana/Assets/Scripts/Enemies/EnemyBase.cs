using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class EnemyBase : MonoBehaviour
{

    // Variables
    [Header("Behaviors")]
    public IAttackBehavior attackBehavior;
    public IUpdateBehavior updateBehavior;
    public IDeathBehavior deathBehavior;
    public ITakeDamageBehavior takeDamageBehavior;

    [Header("VFX")]
    public Animator animator;

    [Header("Components")]
    [HideInInspector]
    public Rigidbody2D rb;

    [Header("Stats")]
    public float speed;
    public int maxHealth;
    public float playerViewRange;

    [Header("Other")]
    [HideInInspector]
    public int health;
    public float viewRange;

    // Constructor
    public EnemyBase()
    {

    }

    // Set Behavior Functions
    public void SetAttackBehaviour(IAttackBehavior atkBehavior)
    {
        attackBehavior = atkBehavior;
    }
    public void SetUpdateBehavior(IUpdateBehavior updBehavior)
    {
        updateBehavior = updBehavior;
    }
    public void SetDeathBehavior(IDeathBehavior detBehavior)
    {
        deathBehavior = detBehavior;
    }
    public void SetTakeDamageBehavior(ITakeDamageBehavior tdmgBehavior)
    {
        takeDamageBehavior = tdmgBehavior;
    }

    // Call Functions
    public void PerformAttack()
    {
        attackBehavior.Attack(gameObject);
    }
    public void PerformUpdate()
    {
        updateBehavior.Update(gameObject);
    }
    public void PerformDeath()
    {
        deathBehavior.Die();
    }

    // Universal Functions
    public void Start()
    {
        health = maxHealth;
    }
    public void Update()
    {
        CheckHealth();
        PerformUpdate();
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        takeDamageBehavior.TakeDamage();
    }
    public void CheckHealth()
    {
        Debug.Log("CheckingHealth");
        if(health <= 0)
        {
            PerformDeath();
        }
    }
}
