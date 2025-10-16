using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCastle : BuildingBase
{
    public static event Action<Team> OnCastleDestroyed; // �C�x���g�ʒm

    protected override void Awake()
    {
        maxHealth = 1500;
        TeamId = Team.Enemy;
        base.Awake();
    }

    protected override void OnConstructed()
    {
        //���邪���������Ƃ��ɌĂт���������������
    }

    protected override void DestroyBuilding()
    {
        OnCastleDestroyed?.Invoke(TeamId);
        Destroy(gameObject);
    }
}
