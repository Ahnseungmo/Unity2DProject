using UnityEngine;

public class Monster : Character
{
    public int monsterId;
    public float speed;
    public float range;

    public void MovePattern()
    {
        // 몬스터 이동 로직
    }

    public int Attack()
    {
        return 10; // 기본 공격력
    }
}
