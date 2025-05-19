using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Vector2Int center; 
    public List<Room> connectedRooms = new List<Room>(); 

    [Header("Portal Settings")]
    public GameObject portalPrefab; 
    public bool isCleared = false;

    public PolygonCollider2D roomBoundsCollider;

    [Header("Enemy Spawning")]
    public List<Transform> enemySpawnPoints = new List<Transform>();

    [HideInInspector]
    public List<GameObject> enemiesInRoom = new List<GameObject>();

    private void Start()
    {
        roomBoundsCollider = GetComponent<PolygonCollider2D>();
    }

    private void Update()
    {
        CheckIfCleared();
    }
    public void CheckIfCleared()
    {
        if (isCleared) return;

        enemiesInRoom.RemoveAll(enemy => enemy == null);

        if (enemiesInRoom.Count == 0)
        {
            isCleared = true;
            SpawnPortal();
        }
    }

    public Transform GetRandomSpawnPoint()
    {
    if (enemySpawnPoints.Count == 0) return null;
    return enemySpawnPoints[Random.Range(0, enemySpawnPoints.Count)];
    }

    private void SpawnPortal()
    {
        if (portalPrefab != null && connectedRooms.Count > 0)
        {
            Vector3 spawnPosition = new Vector3(center.x, center.y, 0f);
            GameObject portal = Instantiate(portalPrefab, spawnPosition, Quaternion.identity, transform);
            
            Portal portalScript = portal.GetComponent<Portal>();
            if (portalScript != null)
            {
                portalScript.Initialize(this); 
            }
        }
    }
}
