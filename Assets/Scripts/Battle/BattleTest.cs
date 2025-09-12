using UnityEngine;

public class BattleTest : MonoBehaviour
{
    public BattleManager battleManager;

    void Start()
    {
        // StageData 임의 생성
        StageData stage = new StageData();

        // 최대 4마리 몬스터 추가
        stage.monsters.Add(new MonsterData("Goblin", 50, 10));
        stage.monsters.Add(new MonsterData("Slime", 30, 5));
        stage.monsters.Add(new MonsterData("Orc", 80, 15));
        stage.monsters.Add(new MonsterData("Bat", 40, 8));

        // 배틀 시작
        battleManager.StartBattle(stage);
    }
}
