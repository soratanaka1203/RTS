using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldMine : BuildingBase
{
    public int goldPerSecond = 10;  // 1•b‚ ‚½‚è‘‚¦‚é—Ê

    protected override void Awake()
    {
        maxHealth = 500;
        TeamId = Team.Player;
        base.Awake();
    }

    protected override void OnConstructed()
    {
        StartCoroutine(ProduceResource());
    }

    private IEnumerator ProduceResource()
    {
        while (true) // ‰i‰“‚ÉŒJ‚è•Ô‚·
        {
            ResourceManager.Instance.AddCoin(goldPerSecond);
            yield return new WaitForSeconds(3f); // 3•b‘Ò‚Â
        }
    }
}
