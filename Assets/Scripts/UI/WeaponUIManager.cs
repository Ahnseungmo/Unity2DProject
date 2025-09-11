using System.Collections.Generic;
using UnityEngine;

public class WeaponUIManager : MonoBehaviour
{
    public static WeaponUIManager Instance;
    public GameObject weaponSlotPrefab;
    public Transform slotParent;

    private Weapon selectedWeapon;

    private void Awake()
    {
        Instance = this;
    }

    public void RefreshUI(List<Weapon> weapons)
    {
        foreach (Transform child in slotParent)
        {
            Destroy(child.gameObject);
        }

        foreach (var w in weapons)
        {
            var slot = Instantiate(weaponSlotPrefab, slotParent);
            slot.GetComponent<WeaponSlotUI>().SetWeapon(w);
        }
    }

    public void SelectWeapon(Weapon w)
    {
        selectedWeapon = w;
        Debug.Log("Selected weapon: " + w.weaponName);
    }

    public Weapon GetSelectedWeapon()
    {
        return selectedWeapon;
    }
}