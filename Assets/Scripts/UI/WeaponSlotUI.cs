using UnityEngine;
using UnityEngine.UI;

public class WeaponSlotUI : MonoBehaviour
{
    public Button button;
    public Text weaponNameText;
    private Weapon weapon;

    public void SetWeapon(Weapon w)
    {
        weapon = w;
        weaponNameText.text = w.weaponName;
        button.onClick.AddListener(() => WeaponUIManager.Instance.SelectWeapon(w));
    }
}