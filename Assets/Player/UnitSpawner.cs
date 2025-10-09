using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private GameObject unitPrefab;

    int unitPrice = 50;
    public void SpawnUnit()
    {
        if (ResourceManager.Instance.coinAmount >= unitPrice)
        {
            Instantiate(unitPrefab, transform.position, Quaternion.identity);
            ResourceManager.Instance.SpendCoin(unitPrice);
        }
        else Debug.Log("ƒRƒCƒ“‚ª‘«‚è‚Ü‚¹‚ñ");
    }
}