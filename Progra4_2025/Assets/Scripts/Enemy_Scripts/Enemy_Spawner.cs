using System.Collections;
using UnityEngine;
public class Enemy_Spawner : MonoBehaviour
{
    [Header("Enemy Settings")]
    public GameObject enemyPrefab;

    [Header("Spawn Settings")]
    public Transform[] spawnPoints;
    public float spawnDelay = 5f;
    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }
    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDelay);
            SpawnEnemy();
        }
    }
    private void SpawnEnemy()
    {
        if (enemyPrefab == null || spawnPoints.Length == 0)
        {
            Debug.LogWarning("Falta el prefab o los puntos de spawn.");
            return;
        }
        Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyPrefab, randomPoint.position, randomPoint.rotation);
    }
}