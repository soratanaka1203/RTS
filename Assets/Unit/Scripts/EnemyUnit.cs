using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : UnitBase
{
    protected override void Awake()
    {
        maxHealth = 150f;

        attackPower = 20f;

        TeamId = Team.Enemy;

        base.Awake(); // UnitBase ‚Ì Awake ‚ğŒÄ‚Ño‚·
        Debug.Log(gameObject.name + " TeamId: " + TeamId);
    }
}
