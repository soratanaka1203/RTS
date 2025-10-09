using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Interface;

public class EnemyUnit : UnitBase
{
    protected override void Awake()
    {
        maxHealth = 150f;

        attackPower = 20f;

        TeamId = Team.Enemy;

        base.Awake(); // UnitBase ÇÃ Awake ÇåƒÇ—èoÇ∑
        Debug.Log(gameObject.name + " TeamId: " + TeamId);
    }

    private void OnTriggerEnter(Collider other)
    {
        IAttackable unit = other.GetComponent<IAttackable>();
        if (unit != null && unit.TeamId != TeamId)
        {
            Debug.Log("ãﬂÇ≠Ç…ìGÇî≠å©");
            attackTarget = unit;
            ChangeState(UnitState.Combat);
        }
    }
}
