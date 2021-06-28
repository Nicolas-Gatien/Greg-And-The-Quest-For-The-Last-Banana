using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Kobold : EnemyBase
{

    // Constructor
    public Kobold()
    {
        updateBehavior = new Patrol(this);
        attackBehavior = new KoboldAttack();
        deathBehavior = new KoboldDeath();
        takeDamageBehavior = new KoboldDamage();
        Init();
    }
    public Kobold(IUpdateBehavior ub, IAttackBehavior ab, IDeathBehavior db, ITakeDamageBehavior tdb)
    {
        updateBehavior = ub;
        attackBehavior = ab;
        deathBehavior = db;
        takeDamageBehavior = tdb;
        Init();
    }
}
