using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Kobold : EnemyBase
{
    // Constructor
    public Kobold()
    {
        updateBehavior = new Patrol(this, 0.5f, 0.75f);
        attackBehavior = new Leap(this);
        deathBehavior = new KoboldDeath();
        takeDamageBehavior = new KoboldDamage();
        animatorBehavior = new GroundedEnemy();
    }
    public Kobold(IUpdateBehavior ub, IAttackBehavior ab, IDeathBehavior db, ITakeDamageBehavior tdb)
    {
        updateBehavior = ub;
        attackBehavior = ab;
        deathBehavior = db;
        takeDamageBehavior = tdb;
    }
}
