using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public List<WeaponInstance> weapons = new List<WeaponInstance>();
    public int currentWeaponIndex = 0;

    public WeaponInstance GetCurrentWeapon()
    {
        if (weapons.Count == 0) return null;
        return weapons[currentWeaponIndex];
    }

    public void SelectWeapon(int index)
    {
        if (index >= 0 && index < weapons.Count)
            currentWeaponIndex = index;
    }

    public int GetCurrentWeaponDamage()
    {
        WeaponInstance weapon = GetCurrentWeapon();
        return weapon != null ? weapon.GetDamage() : 0;
    }

    public void ResetWeapons()
    {
        foreach (var weapon in weapons)
            weapon.Used = false;
    }
}
