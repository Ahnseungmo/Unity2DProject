using UnityEngine;

[System.Serializable]
public class Weapon
{
    public string weaponName;
    public int damage;

    public Weapon(string name, int dmg)
    {
        weaponName = name;
        damage = dmg;
    }
}