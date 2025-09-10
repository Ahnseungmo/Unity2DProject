using UnityEngine;
using UnityEngine.UI;

public class WeaponSlotUI : MonoBehaviour
{
    public int weaponIndex;
    public Text nameText;
    public Button button;

    public void UpdateSlotUI(WeaponInstance weapon, bool isSelected)
    {
        nameText.text = weapon.Data.Name;

        ColorBlock colors = button.colors;
        colors.normalColor = isSelected ? Color.yellow : Color.white;
        button.colors = colors;
    }

    public void OnClick()
    {
        PlayerInventory.Instance.SelectWeapon(weaponIndex);
        WeaponUIManager.Instance.UpdateUI();
    }
}
