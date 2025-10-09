using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCastle : BuildingBase
{
    protected override void Awake()
    {
        maxHealth = 1500;
        TeamId = Team.Enemy;
        base.Awake();
    }

    protected override void OnConstructed()
    {
        
    }
}
