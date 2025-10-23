using System;
using System.Collections;
using UnityEngine;

public class Castle : BuildingBase
{
    public int goldPerSecond = 10;  // 1•b‚ ‚½‚è‘‚¦‚é—Ê
    public static event Action<Team> OnCastleDestroyed; // ƒCƒxƒ“ƒg’Ê’m

    protected override void Awake()
    {
        maxHealth = 1500;
        TeamId = Team.Player;
        base.Awake();
    }

    //‚¨é‚ªŠ®¬‚µ‚½‚Æ‚«‚ÉŒÄ‚Î‚ê‚éˆ—
    protected override void OnConstructed()
    {
        StartCoroutine(ProduceResource());
    }

    private IEnumerator ProduceResource()
    {
        while (true) // ‰i‰“‚ÉŒJ‚è•Ô‚·
        {
            ResourceManager.Instance.AddCoin(goldPerSecond);
            yield return new WaitForSeconds(1f); // 1•b‘Ò‚Â
        }
    }
    protected override void DestroyBuilding()
    {
        OnCastleDestroyed?.Invoke(TeamId);
        Destroy(gameObject);
    }
}
