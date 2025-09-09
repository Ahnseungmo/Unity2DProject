using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public List<Weapon> weapons = new List<Weapon>();
    public int currentWeaponIndex = 0;

    public Weapon GetCurrentWeapon() => weapons[currentWeaponIndex];

    public void SelectWeapon(int index)
    {
        if (index >= 0 && index < weapons.Count)
        {
            currentWeaponIndex = index;
            Debug.Log($"선택된 무기: {GetCurrentWeapon().Name}");
            WeaponUIManager.Instance.UpdateUI();
        }
    }

    public bool UseAmmo()
    {
        Weapon weapon = GetCurrentWeapon();
        return weapon.UseAmmo();
    }

    public int GetCurrentWeaponDamage()
    {
        return GetCurrentWeapon().Damage;
    }
}
