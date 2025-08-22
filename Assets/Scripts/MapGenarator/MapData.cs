using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
[System.Serializable]
public class MapNode
{
    public int id;
    public Vector2Int position;   // UI ��ǥ
    public NodeType type;      // ����, ����, �̺�Ʈ ��
//    public List<MapNode> connections = new List<MapNode>();
    public List<MapNode> children;
    public List<MapNode> parent;
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
