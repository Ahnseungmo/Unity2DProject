using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterData
{
    public string monsterId;
    public int hp;
    public int attack;

    public MonsterData(string id, int hp, int atk)
    {
        this.monsterId = id;
        this.hp = hp;
        this.attack = atk;
    }
}

[System.Serializable]
public class StageData
{
    public List<MonsterData> monsters = new List<MonsterData>();
}

[System.Serializable]
public class WeaponData
{
    public string weaponId;
    public int damage;
    public int count; // 인벤토리 내 개수
}


public class DataManager : Singleton<DataManager>
{
    public StageData stageData;
    public Player player;

}
