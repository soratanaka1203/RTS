using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : UnitBase
{
    protected override void Awake()
    {
        base.Awake(); // UnitBase �� Awake ���Ăяo��

        maxHealth = 150f;
        currentHealth = maxHealth;

        attackPower = 20f;
    }
}
