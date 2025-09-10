using UnityEngine;

public class WeaponUIManager : MonoBehaviour
{
    public static WeaponUIManager Instance { get; private set; }

    public Transform slotParent;
    public GameObject slotPrefab;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void CreateSlot(int index, WeaponInstance weapon)
    {
        GameObject slotObj = Instantiate(slotPrefab, slotParent);
        WeaponSlotUI slotUI = slotObj.GetComponent<WeaponSlotUI>();
        slotUI.weaponIndex = index;
        slotUI.UpdateSlotUI(weapon, false);
    }

    public void UpdateUI()
    {
        WeaponSlotUI[] slots = slotParent.GetComponentsInChildren<WeaponSlotUI>();
        for (int i = 0; i < slots.Length; i++)
        {
            WeaponInstance weapon = PlayerInventory.Instance.weapons[i];
            bool isSelected = (i == PlayerInventory.Instance.currentWeaponIndex);
            slots[i].UpdateSlotUI(weapon, isSelected);
        }
    }
}
