using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCastle : BuildingBase
{
    public static event Action<Team> OnCastleDestroyed; // イベント通知

    protected override void Awake()
    {
        maxHealth = 1500;
        TeamId = Team.Enemy;
        base.Awake();
    }

    protected override void OnConstructed()
    {
        //お城が完成したときに呼びたい処理をここに
    }

    protected override void DestroyBuilding()
    {
        OnCastleDestroyed?.Invoke(TeamId);
        Destroy(gameObject);
    }
}
