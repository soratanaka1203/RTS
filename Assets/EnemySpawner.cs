using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Interface;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private BuildingBase playerCastle;
    float spawnInterval = 7f;

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval) ;
            EnemyUnit enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity).GetComponent<EnemyUnit>();
            enemy.defaultTarget = playerCastle;
        }
    }
}
