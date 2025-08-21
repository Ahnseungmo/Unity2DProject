using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    [Header("References")]
    public RectTransform mapParent;   // 노드를 붙일 부모 (Canvas 내 Panel)
    public GameObject nodePrefab;     // UI 노드 프리팹
    public GameObject linePrefab;     // 선 프리팹 (Image 사용)

    [Header("Config")]
    public int totalFloors = 10;
    public Vector2 nodeSpacing = new Vector2(200, 150);

    private List<MapNodeUI> allNodes = new List<MapNodeUI>();

    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        int nodeId = 0;

        for (int f = 0; f < totalFloors; f++)
        {
            int nodeCount = Random.Range(2, 5);
            List<MapNodeUI> floorNodes = new List<MapNodeUI>();

            for (int i = 0; i < nodeCount; i++)
            {
                Vector2 pos = new Vector2(i * nodeSpacing.x, -f * nodeSpacing.y);

                GameObject obj = Instantiate(nodePrefab, mapParent);
                obj.GetComponent<RectTransform>().anchoredPosition = pos;

                MapNodeUI node = obj.GetComponent<MapNodeUI>();
                node.Init(nodeId++, f, pos);

                allNodes.Add(node);
                floorNodes.Add(node);
            }

            // 연결
            if (f > 0)
            {
                var prevFloorNodes = allNodes.FindAll(n => n.floor == f - 1);

                foreach (var prev in prevFloorNodes)
                {
                    foreach (var current in floorNodes)
                    {
                        if (Mathf.Abs(prev.position.x - current.position.x) < nodeSpacing.x * 1.5f)
                        {
                            if (Random.value < 0.6f)
                            {
                                CreateConnection(prev, current);
                            }
                        }
                    }
                }
            }
        }
    }

    void CreateConnection(MapNodeUI from, MapNodeUI to)
    {
        GameObject line = Instantiate(linePrefab, mapParent);
        UILineConnector connector = line.GetComponent<UILineConnector>();
        connector.Connect(from.GetComponent<RectTransform>(), to.GetComponent<RectTransform>());
    }
}
