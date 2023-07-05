using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyFlyweightPointer
{
    public static readonly EnemyFlyweight AIController = new EnemyFlyweight
    {
        attackers = new int[] {6},
        damageRate = 0.25f,
        returnToWhiteColorTimer = 0.5f,
        DeadTriggerName = "Die"
    };
    
    public static readonly EnemyFlyweight GuardAIController = new EnemyFlyweight
    {
        movementSpeed = 4f,
        chaseDistance = 5f,
    };

    public static readonly EnemyFlyweight StoneBossAIController = new EnemyFlyweight
    {
        movementSpeed = 2f,
        chaseDistance = 6.5f,
        damageRate = 0.35f,
        returnToWhiteColorTimer = 0,
    };
}
