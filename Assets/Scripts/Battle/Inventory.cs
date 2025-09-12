using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Tooltip("매 턴 고정으로 사용할 무기 템플릿들")]
    public List<WeaponTemplate> weaponTemplates = new List<WeaponTemplate>();

    private Queue<WeaponTemplate> turnWeapons = new Queue<WeaponTemplate>();

    public void RefillWeapons()
    {
        turnWeapons.Clear();
        foreach (var w in weaponTemplates)
            turnWeapons.Enqueue(w);
    }

    public WeaponTemplate GetNextWeapon()
    {
        if (turnWeapons.Count == 0) return null;
        return turnWeapons.Dequeue();
    }

    public bool HasWeapons() => turnWeapons.Count > 0;
}
