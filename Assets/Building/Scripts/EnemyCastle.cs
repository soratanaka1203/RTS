using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCastle : BuildingBase
{
    protected override void Awake()
    {
        maxHealth = 1500;
        base.Awake();
    }

    protected override void OnConstructed()
    {
        Debug.Log("�邪�����I���\�[�X�𐶎Y�J�n���܂��B");
    }
}
