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
    public IAnimatorBehavior animatorBehavior;

    [Header("VFX")]
    public Animator animator;
    public GameObject[] deathEffects;

    [Header("Components")]
    [HideInInspector]
    public Rigidbody2D rb;

    [Header("Stats")]
    public float speed;
    public int maxHealth;
    public float playerViewRange;

    [Header("Other")]
    public int health;
    public float viewRange;

    // Functionality Variables
    bool hasDied;

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
    public void SetAnimatorBehavior(IAnimatorBehavior animBehavior)
    {
        animatorBehavior = animBehavior;
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
    public void SetAnimations()
    {
        animatorBehavior.Animate(animator, gameObject);
    }

    // Universal Functions
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = maxHealth;
    }
    public void Update()
    {
        CheckHealth();
        PerformUpdate();
        SetAnimations();
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        takeDamageBehavior.TakeDamage();
    }
    public void CheckHealth()
    {
        if(health <= 0 && !hasDied)
        {
            hasDied = true;
            PerformDeath();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Death"))
        {
            TakeDamage(1);
        }
    }
    
}

