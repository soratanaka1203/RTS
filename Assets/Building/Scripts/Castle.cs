using System;
using System.Collections;
using UnityEngine;

public class Castle : BuildingBase
{
    public int goldPerSecond = 10;  // 1�b�����葝�����
    public static event Action<Team> OnCastleDestroyed; // �C�x���g�ʒm

    protected override void Awake()
    {
        maxHealth = 1500;
        TeamId = Team.Player;
        base.Awake();
    }

    protected override void OnConstructed()
    {
        Debug.Log("�邪�����I���\�[�X�𐶎Y�J�n���܂��B");
        StartCoroutine(ProduceResource());
    }

    private IEnumerator ProduceResource()
    {
        while (true) // �i���ɌJ��Ԃ�
        {
            ResourceManager.Instance.AddCoin(goldPerSecond);
            yield return new WaitForSeconds(1f); // 1�b�҂�
        }
    }
    protected override void DestroyBuilding()
    {
        OnCastleDestroyed?.Invoke(TeamId);
        Destroy(gameObject);
    }
}
