using UnityEngine;

public class BattleTest : MonoBehaviour
{
    public BattleManager battleManager;
    private StageData stage;
    void Start()
    {
        if(DataManager.Get.stageData == null)
        {
            // StageData 임의 생성
            stage = new StageData();
            Player player = new Player();
            //        player.Init("Player", 100, 5);
            // 최대 4마리 몬스터 추가
            stage.monsters.Add(new MonsterData("Goblin", 50, 10));
            stage.monsters.Add(new MonsterData("Slime", 30, 5));
            stage.monsters.Add(new MonsterData("Orc", 80, 15));
            stage.monsters.Add(new MonsterData("Bat", 40, 8));
        }
        else
        {
            // 배틀 시작
            //        battleManager.playerData = player;
            battleManager.StartBattle(DataManager.Get.stageData);
        }



    }
}
