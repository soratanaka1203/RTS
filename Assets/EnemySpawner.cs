using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Interface;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private BuildingBase playerCastle;
    float spawnInterval = 5f;

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval) ;
            UnitBase enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity).GetComponent<UnitBase>();
            enemy.SetTarget(playerCastle);
        }
    }
}
