using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance;

    public int playerHP = 100;
    public List<Weapon> weapons = new List<Weapon>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SavePlayerState(Player player)
    {
        playerHP = player.hp;
        weapons = new List<Weapon>(player.Inventory.weapons);
    }

    public void LoadPlayerState(Player player)
    {
        player.hp = playerHP;
        player.Inventory.weapons.Clear();
        foreach (var w in weapons)
        {
            player.Inventory.AddWeapon(w);
        }
    }
}