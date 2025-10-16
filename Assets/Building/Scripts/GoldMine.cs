using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldMine : BuildingBase
{
    public int goldPerSecond = 10;  // 1�b�����葝�����

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
        while (true) // �i���ɌJ��Ԃ�
        {
            ResourceManager.Instance.AddCoin(goldPerSecond);
            yield return new WaitForSeconds(3f); // 3�b�҂�
        }
    }
}
