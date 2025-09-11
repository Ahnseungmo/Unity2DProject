using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<Weapon> weapons = new List<Weapon>();

    public void AddWeapon(Weapon weapon)
    {
        weapons.Add(weapon);
    }

    public void RemoveWeapon(Weapon weapon)
    {
        weapons.Remove(weapon);
    }
}