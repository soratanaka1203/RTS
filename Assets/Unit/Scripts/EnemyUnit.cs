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
        defensePower = 5f;

        TeamId = Team.Enemy;

        base.Awake(); // UnitBase �� Awake ���Ăяo��
    }

    private void OnTriggerEnter(Collider other)
    {
        IAttackable unit = other.GetComponent<IAttackable>();
        if (unit != null && unit.TeamId != TeamId)
        {
            Debug.Log("�߂��ɓG�𔭌�");
            SetTarget(unit);
            ChangeState(UnitState.Combat);
        }
    }
}
