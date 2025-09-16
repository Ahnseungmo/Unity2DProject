using System.Collections.Generic;
using DanielLochner.Assets.SimpleScrollSnap;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Tooltip("매 턴 고정으로 사용할 무기 템플릿들")]
    public List<WeaponTemplate> weaponTemplates = new List<WeaponTemplate>();

    private Queue<WeaponTemplate> turnWeapons = new Queue<WeaponTemplate>();
    public InventoryContent InventoryUI;

    public void RefillWeapons()
    {
        turnWeapons.Clear();
        foreach (var w in weaponTemplates)
        {
            var sr = w.projectilePrefab.GetComponent<SpriteRenderer>();
            InventoryUI.AddToBack(sr.sprite);
            turnWeapons.Enqueue(w);

        }
    }

    public WeaponTemplate GetNextWeapon()
    {

        if (turnWeapons.Count == 0) return null;
        InventoryUI.RemoveFromFront();
        return turnWeapons.Dequeue();
    }

    public bool HasWeapons() => turnWeapons.Count > 0;
}
