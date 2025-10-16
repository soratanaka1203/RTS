using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Interface;

public class SoldierUnit : UnitBase
{
    protected override void Awake()
    {
        maxHealth = 150f;

        attackPower = 20f;
        defensePower = 5f;

        TeamId = Team.Player;

        base.Awake(); // UnitBase ÇÃ Awake ÇåƒÇ—èoÇ∑
    }

    private void OnTriggerEnter(Collider other)
    {
        IAttackable unit = other.GetComponent<IAttackable>();
        if (unit != null && unit.TeamId != TeamId)
        {
            SetTarget(unit);
            ChangeState(UnitState.Combat);
        }
    }
}
