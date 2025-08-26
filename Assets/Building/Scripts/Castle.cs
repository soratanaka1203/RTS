using System.Collections;
using UnityEngine;

public class Castle : BuildingBase
{
    public int goldPerSecond = 10;  // 1•b‚ ‚½‚è‘‚¦‚é—Ê

    protected override void Awake()
    {
        maxHealth = 1500;
        base.Awake();
    }

    protected override void OnConstructed()
    {
        Debug.Log("é‚ªŠ®¬IƒŠƒ\[ƒX‚ğ¶YŠJn‚µ‚Ü‚·B");
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
}
