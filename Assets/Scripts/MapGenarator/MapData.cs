using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class MapNode
{
    public int id;
    public int floor;          // 몇 번째 층인지
    public Vector2 position;   // UI 좌표
    public NodeType type;      // 전투, 상점, 이벤트 등
    public List<MapNode> connections = new List<MapNode>();
}

public enum NodeType
{
    Enemy, Elite, Shop, Event, Rest, Boss
}

[System.Serializable]
public class MapFloor
{
    public int floorIndex;
    public List<MapNode> nodes = new List<MapNode>();
}

public class MapData
{
    public List<MapFloor> floors = new List<MapFloor>();
}
