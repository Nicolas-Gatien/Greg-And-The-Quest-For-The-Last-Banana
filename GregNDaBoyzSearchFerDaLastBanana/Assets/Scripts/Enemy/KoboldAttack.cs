using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoboldAttack : IAttackBehavior
{
    public void Attack()
    {
        Debug.Log("I am attacking");
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
