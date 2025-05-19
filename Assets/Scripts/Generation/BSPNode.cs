using UnityEngine;

public class BSPNode {
    public RectInt area;
    public Vector2Int roomPosition;
    public BSPNode left;
    public BSPNode right;

    public bool IsLeaf => left == null && right == null;

    public Vector2Int GetRoomCenter() 
    {
        return roomPosition;
    }
}
