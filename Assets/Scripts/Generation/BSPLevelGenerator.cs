
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BSPLevelGenerator : MonoBehaviour {
    [Header("Room Prefabs")]
    public GameObject[] roomPrefabs;
    public GameObject playerPrefab;

    [Header("Map Settings")]
    public int mapWidth = 100;
    public int mapHeight = 100;
    public int minRoomSize = 20;
    public int minRoomCount = 7;

    private BSPNode rootNode;
    private List<BSPNode> leaves;
    private List<Room> roomInstances = new List<Room>();

    [Header("Enemy Settings")]
    public EnemySpawner enemySpawner;
    public int minEnemiesPerRoom = 1;
    public int maxEnemiesPerRoom = 4;

    void Start() 
    {
        RectInt rootRect = new RectInt(0, 0, mapWidth, mapHeight);
        rootNode = new BSPNode { area = rootRect };
        leaves = new List<BSPNode>();

        SplitUntilMinRooms(rootNode);
        InstantiateRooms();
        SpawnEnemies();
        ConnectRooms(rootNode);
        EnsureAllRoomsConnected();
    }

    void SplitUntilMinRooms(BSPNode root) 
    {
        Queue<BSPNode> queue = new Queue<BSPNode>();
        queue.Enqueue(root);
        int attempts = 0;

        while (queue.Count > 0 && leaves.Count < minRoomCount && attempts < 1000) 
        {
            BSPNode node = queue.Dequeue();

            if (SplitNode(node)) 
            {
                queue.Enqueue(node.left);
                queue.Enqueue(node.right);
            } else 
            {
                leaves.Add(node);
            }

            attempts++;
        }
        
    }

    bool SplitNode(BSPNode node) {
        if (node.area.width < minRoomSize * 2 || node.area.height < minRoomSize * 2)
            return false;

        bool splitHorizontally = Random.value > 0.5f;

        if (node.area.width > node.area.height)
            splitHorizontally = false;
        else if (node.area.height > node.area.width)
            splitHorizontally = true;

        if (splitHorizontally) 
        {
            int splitY = Random.Range(minRoomSize, node.area.height - minRoomSize);
            node.left = new BSPNode 
            {
                area = new RectInt(node.area.x, node.area.y, node.area.width, splitY)
            };
            node.right = new BSPNode 
            {
                area = new RectInt(node.area.x, node.area.y + splitY, node.area.width, node.area.height - splitY)
            };
        } else 
        {
            int splitX = Random.Range(minRoomSize, node.area.width - minRoomSize);
            node.left = new BSPNode 
            {
                area = new RectInt(node.area.x, node.area.y, splitX, node.area.height)
            };
            node.right = new BSPNode 
            {
                area = new RectInt(node.area.x + splitX, node.area.y, node.area.width - splitX, node.area.height)
            };
        }

        return true;
    }

    void InstantiateRooms() 
    {
        bool firstRoomFound = false;

        foreach (var leaf in leaves) {
            GameObject roomGO = Instantiate(
                roomPrefabs[Random.Range(0, roomPrefabs.Length)],
                new Vector3(leaf.area.x + leaf.area.width / 2, leaf.area.y + leaf.area.height / 2, 0),
                Quaternion.identity
            );

            var room = roomGO.GetComponent<Room>();
            room.center = new Vector2Int((int)roomGO.transform.position.x, (int)roomGO.transform.position.y);
            roomInstances.Add(room);

            leaf.roomPosition = room.center;

            if (!firstRoomFound && playerPrefab != null) 
            {
                 GameObject player = Instantiate(playerPrefab, roomGO.transform.position, Quaternion.identity);


                CameraConfinerHandler confinerHandler = Camera.main.GetComponent<CameraConfinerHandler>();
                if (confinerHandler != null)
                    confinerHandler.SetConfinerBounds(room.GetComponent<PolygonCollider2D>());

                firstRoomFound = true;
            }
        }
    }

    void ConnectRooms(BSPNode node) 
    {
        if (node == null || node.IsLeaf) return;

        ConnectRooms(node.left);
        ConnectRooms(node.right);

        Vector2Int a = node.left.GetRoomCenter();
        Vector2Int b = node.right.GetRoomCenter();

        Room roomA = FindRoomAt(a);
        Room roomB = FindRoomAt(b);

        if (roomA != null && roomB != null && !roomA.connectedRooms.Contains(roomB)) 
        {
            roomA.connectedRooms.Add(roomB);
            roomB.connectedRooms.Add(roomA);
        }
    }

    Room FindRoomAt(Vector2Int pos) 
    {
        foreach (var r in roomInstances) 
        {
            if (r.center == pos) return r;
        }
        return null;
    }

    void EnsureAllRoomsConnected() 
    {
        HashSet<Room> visited = new HashSet<Room>();
        Queue<Room> queue = new Queue<Room>();

        if (roomInstances.Count == 0) return;

        Room start = roomInstances[0];
        visited.Add(start);
        queue.Enqueue(start);

        while (queue.Count > 0) {
            Room current = queue.Dequeue();
            foreach (Room neighbor in current.connectedRooms) 
            {
                if (!visited.Contains(neighbor)) 
                {
                    visited.Add(neighbor);
                    queue.Enqueue(neighbor);
                }
            }
        }

        foreach (Room room in roomInstances) 
        {
            if (!visited.Contains(room)) 
            {
                Room closest = null;
                float minDistance = float.MaxValue;

                foreach (Room connected in visited) 
                {
                    float dist = Vector2.Distance(room.center, connected.center);
                    if (dist < minDistance) {
                        minDistance = dist;
                        closest = connected;
                    }
                }

                if (closest != null) 
                {
                    room.connectedRooms.Add(closest);
                    closest.connectedRooms.Add(room);
                    visited.Add(room);
                    queue.Enqueue(room);
               
                }
            }
        }
    }
    void SpawnEnemies()
    {
        foreach (Room room in roomInstances)
        {
            if (room.enemiesInRoom == null || room.enemySpawnPoints.Count == 0)
                continue;

            int enemiesToSpawn = Random.Range(minEnemiesPerRoom, Mathf.Min(maxEnemiesPerRoom + 1, room.enemySpawnPoints.Count + 1));
            enemySpawner.SpawnEnemiesInRoom(room, enemiesToSpawn);
        }
    }
}