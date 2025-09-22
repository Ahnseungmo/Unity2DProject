/*
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEditor.SceneManagement;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;
*/
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class NodePrefab : MonoBehaviour,IPointerClickHandler
{
    private MapNodeComponent nodeComponent;

    public bool clickAble = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nodeComponent = GetComponent<MapNodeComponent>();
        var playerNode = MapGenerator.Get.AllNodes.Find(s => s.Pos == DataManager.Get.stagePos);
        var findNode = playerNode.mapNodeComponent.Children.Find(s => s.Data.Pos == nodeComponent.Data.Pos);
        if (findNode == null) clickAble = false;
        else clickAble = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData data)
    {
        print("노드 클릭");
        if (!clickAble || !DataManager.Get.moveAble) return;
        
        DataManager.Get.stagePos = nodeComponent.Data.Pos;

        GameObject.Find("Character").GetComponent<MapCharacter>().MoveToPosition(this);
        DataManager.Get.moveAble = false;
    }
    private void ClickAbleChirdren()
    {
        foreach(var node in nodeComponent.Children)
        {
//            node.Children.gameobject
        }
    }

    public void NodeStart()
    {
        DataManager.Get.moveAble = true;
        DataManager.Get.stagePos = nodeComponent.Data.Pos;

        switch (nodeComponent.Data.mapType)
        {
            case MapType.Home:

                SceneManager.LoadScene("MapScene");
                break;
            case MapType.Battle:

                StageData stage = new StageData();
                //        player.Init("Player", 100, 5);
                // 최대 4마리 몬스터 추가
                stage.monsters.Add(new MonsterData("Goblin", 50, 10));
                //               stage.monsters.Add(new MonsterData("Slime", 30, 5));
                //                stage.monsters.Add(new MonsterData("Orc", 80, 15));
                //                stage.monsters.Add(new MonsterData("Bat", 40, 8));

                DataManager.Get.stageData = stage;
                SceneManager.LoadScene("BattleScene");
                break;
            case MapType.Boss:
                break;
            default:
                break;
        }
    }
}
