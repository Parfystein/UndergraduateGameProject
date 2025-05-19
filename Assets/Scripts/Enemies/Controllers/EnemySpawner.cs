using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Tooltip("Enemy prefabs to randomly spawn")]
    public GameObject[] enemyPrefabs;

    public void SpawnEnemiesInRoom(Room room, int count)
    {
        if (room.enemySpawnPoints.Count == 0 || enemyPrefabs.Length == 0)
            return;

        count = Mathf.Min(count, room.enemySpawnPoints.Count);

        List<Transform> shuffled = new List<Transform>(room.enemySpawnPoints);
        for (int i = 0; i < shuffled.Count; i++)
        {
            int swapIndex = Random.Range(i, shuffled.Count);
            (shuffled[i], shuffled[swapIndex]) = (shuffled[swapIndex], shuffled[i]);
        }

        for (int i = 0; i < count; i++)
        {
            Transform spawnPoint = shuffled[i];
            GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity, transform);

            room.enemiesInRoom.Add(enemy);
        }
    }
}
