using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

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

    public void AddWeapon(WeaponInstance newWeapon)
    {
        weapons.Add(newWeapon);
        WeaponUIManager.Instance.CreateSlot(weapons.Count - 1, newWeapon);
        WeaponUIManager.Instance.UpdateUI();
    }
}
