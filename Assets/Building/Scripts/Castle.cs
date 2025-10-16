using System;
using System.Collections;
using UnityEngine;

public class Castle : BuildingBase
{
    public int goldPerSecond = 10;  // 1秒あたり増える量
    public static event Action<Team> OnCastleDestroyed; // イベント通知

    protected override void Awake()
    {
        maxHealth = 1500;
        TeamId = Team.Player;
        base.Awake();
    }

    protected override void OnConstructed()
    {
        Debug.Log("城が完成！リソースを生産開始します。");
        StartCoroutine(ProduceResource());
    }

    private IEnumerator ProduceResource()
    {
        while (true) // 永遠に繰り返す
        {
            ResourceManager.Instance.AddCoin(goldPerSecond);
            yield return new WaitForSeconds(1f); // 1秒待つ
        }
    }
    protected override void DestroyBuilding()
    {
        OnCastleDestroyed?.Invoke(TeamId);
        Destroy(gameObject);
    }
}
