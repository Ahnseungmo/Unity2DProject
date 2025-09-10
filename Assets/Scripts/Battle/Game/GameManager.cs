using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Player player;
    public List<Monster> monsters = new List<Monster>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void DamageMonster(int monsterId, int damage)
    {
        Monster monster = monsters.Find(m => m.monsterId == monsterId);
        if (monster != null)
        {
            monster.TakeDamage(damage);
            Debug.Log($"{monster.characterName} took {damage} damage!");
        }

        CheckPlayerTurnEnd();
    }

    public void CheckPlayerTurnEnd()
    {
        bool allUsed = true;
        foreach (var weapon in player.weapons)
        {
            if (!weapon.Used)
            {
                allUsed = false;
                break;
            }
        }

        if (allUsed)
        {
            EndPlayerTurn();
        }
    }

    public void EndPlayerTurn()
    {
        foreach (var monster in monsters)
        {
            int damage = monster.Attack();
            player.TakeDamage(damage);
            Debug.Log($"{monster.characterName} attacked player for {damage} damage!");
        }

        RefillPlayerWeapons();
    }

    public void RefillPlayerWeapons()
    {
        player.ResetWeapons();
        Debug.Log("Weapons recharged for next turn!");
    }
}
