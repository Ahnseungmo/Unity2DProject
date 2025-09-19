using System.Collections.Generic;
using UnityEngine;

public class MapNodeComponent : MonoBehaviour
{
    public MapNodeData Data;
    public List<MapNodeComponent> Children = new List<MapNodeComponent>();
    public List<MapNodeComponent> Parents = new List<MapNodeComponent>();
}

public class StageManager : MonoBehaviour
{
    [Header("Reference")]
    public Grid Map;
    public GameObject nodePrefab;
    public GameObject linePrefab;
    public Sprite Home;
    public Sprite Battle;
    public Sprite Boss;
    public MapCharacter character;

    [Header("Config")]
    public int TotalFloors;
    public int FloorWeight;
    public int RootSize;

    private Vector2Int startStagePos = new Vector2Int(0,-1);
    //  MapGenerator mapGenerator = new MapGenerator();

    private Dictionary<MapNodeData, MapNodeComponent> nodeComponentMap = new Dictionary<MapNodeData, MapNodeComponent>();

    void Start()
    {
        if(DataManager.Get.stagePos == startStagePos)
            MapGenerator.Get.Generate(TotalFloors, FloorWeight, RootSize);


        // 1) 데이터 -> GameObject + 컴포넌트 생성 및 위치 지정
        foreach (var nodeData in MapGenerator.Get.AllNodes)
        {
            GameObject obj = Instantiate(nodePrefab, Map.transform);
            Vector3 pos = Map.GetCellCenterLocal(new Vector3Int(nodeData.Pos.x, nodeData.Pos.y, 0));
            obj.transform.position = pos;

            MapNodeComponent comp = obj.GetComponent<MapNodeComponent>();
            if (comp == null) comp = obj.AddComponent<MapNodeComponent>();

            comp.Data = nodeData;
            nodeComponentMap[nodeData] = comp;
            comp.Data.mapNodeComponent = comp;
            Sprite sp;
            switch (comp.Data.mapType)
            {
                case MapType.Home:
                    sp = Home;
                    break;
                case MapType.Battle:
                    sp = Battle;
                    break;
                case MapType.Boss:
                    sp = Boss;
                    break;
                default:
                    sp = null;
                    break;
            }
            obj.GetComponent<SpriteRenderer>().sprite = sp;
        }

        // 2) 부모-자식 컴포넌트 연결
        foreach (var nodeData in MapGenerator.Get.AllNodes)
        {
            MapNodeComponent comp = nodeComponentMap[nodeData];
            foreach (var childData in nodeData.Children)
            {
                MapNodeComponent childComp = nodeComponentMap[childData];
                comp.Children.Add(childComp);
                childComp.Parents.Add(comp);
            }
        }

        // 3) 선 생성 (부모-자식 연결선)
        foreach (var nodeComp in nodeComponentMap.Values)
        {
            foreach (var childComp in nodeComp.Children)
            {
                GameObject lineObj = Instantiate(linePrefab, Map.transform);

                Vector3 startPos = nodeComp.transform.position;
                Vector3 endPos = childComp.transform.position;

                Vector3 dir = endPos - startPos;
                float dist = dir.magnitude;

                lineObj.transform.position = startPos + dir / 2;

                RectTransform rt = lineObj.GetComponent<RectTransform>();
                if (rt != null) rt.sizeDelta = new Vector2(dist, rt.sizeDelta.y);

                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                lineObj.transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }

        character.SetPos(DataManager.Get.stagePos);
    }
}
