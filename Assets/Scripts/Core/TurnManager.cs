using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;
    private bool playerTurn = true;

    private void Awake()
    {
        Instance = this;
    }

    public void EndPlayerTurn()
    {
        playerTurn = false;
        EnemyTurn();
    }

    private void EnemyTurn()
    {
        // 몬스터들의 공격 처리
        Monster[] monsters = FindObjectsOfType<Monster>();
        foreach (var m in monsters)
        {
            m.AttackPlayer();
        }

        playerTurn = true;
    }
}