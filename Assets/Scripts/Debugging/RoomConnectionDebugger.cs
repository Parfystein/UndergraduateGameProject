using UnityEngine;
using System.Collections.Generic;

public class RoomConnectionDebugger : MonoBehaviour {
    void OnDrawGizmos() {
        Room[] allRooms = FindObjectsOfType<Room>();
        Gizmos.color = Color.red;

        foreach (Room room in allRooms) 
        {
            if (room == null || room.connectedRooms == null) continue;

            foreach (Room connected in room.connectedRooms) 
            {
             
                if (room.center.x < connected.center.x || (room.center.x == connected.center.x && room.center.y < connected.center.y)) 
                {
                    Gizmos.DrawLine(new Vector3(room.center.x, room.center.y, 0), new Vector3(connected.center.x, connected.center.y, 0));
                }
            }
        }
    }
}
