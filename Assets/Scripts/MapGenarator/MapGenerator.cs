using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapNodeData
{
    public Vector2Int Pos;
    public List<MapNodeData> Children = new List<MapNodeData>();
    public List<MapNodeData> Parents = new List<MapNodeData>();
}

public class MapGenerator
{
    public List<MapNodeData> AllNodes = new List<MapNodeData>();

    // ���� ����ũ ���� ���� �Լ�
    private List<int> GetUniqueRandomNumbers(int count, int min, int max)
    {
        HashSet<int> numbers = new HashSet<int>();
        while (numbers.Count < count)
        {
            numbers.Add(Random.Range(min, max));
        }
        return numbers.ToList();
    }

    public void Generate(int totalFloors, int floorWeight, int rootSize)
    {
        AllNodes.Clear();

        List<int> startXPositions = GetUniqueRandomNumbers(rootSize, 0, floorWeight);

        // �� root �� ��ǥ ����Ʈ ���� (y ������ 0~totalFloors-1)
        List<List<Vector2Int>> roots = new List<List<Vector2Int>>();

        for (int i = 0; i < rootSize; i++)
        {
            List<Vector2Int> rootPath = new List<Vector2Int>();

            for (int y = 0; y < totalFloors; y++)
            {
                int x;
                if (y == 0)
                {
                    x = startXPositions[i];
                }
                else
                {
                    Vector2Int prevPos = rootPath.Last();

                    int minOffset = (prevPos.x <= 0) ? 0 : -1;
                    int maxOffset = (prevPos.x >= floorWeight - 1) ? 0 : 1;

                    x = Random.Range(prevPos.x + minOffset, prevPos.x + maxOffset + 1);
                }

                rootPath.Add(new Vector2Int(x, y));
            }

            roots.Add(rootPath);
        }

        // ��� ��ǥ�� MapNodeData�� ��ȯ �� �ߺ� ���� ���� ��ųʸ� ���
        Dictionary<(int, int), MapNodeData> nodeDict = new Dictionary<(int, int), MapNodeData>();

        foreach (var root in roots)
        {
            foreach (var pos in root)
            {
                var key = (pos.x, pos.y);
                if (!nodeDict.ContainsKey(key))
                {
                    MapNodeData node = new MapNodeData
                    {
                        Pos = pos
                    };
                    nodeDict.Add(key, node);
                    AllNodes.Add(node);
                }
            }
        }

        // �θ�-�ڽ� ���� ���� (y+1 ���� y �� ����)
        foreach (var root in roots)
        {
            for (int i = 1; i < root.Count; i++)
            {
                var parentKey = (root[i - 1].x, root[i - 1].y);
                var childKey = (root[i].x, root[i].y);

                MapNodeData parentNode = nodeDict[parentKey];
                MapNodeData childNode = nodeDict[childKey];

                if (!parentNode.Children.Contains(childNode))
                    parentNode.Children.Add(childNode);

                if (!childNode.Parents.Contains(parentNode))
                    childNode.Parents.Add(parentNode);
            }
        }
    }
}
