using UnityEngine;

[CreateAssetMenu(menuName = "Battle/Weapon")]
public class WeaponTemplate : ScriptableObject
{
    public string weaponName;
    public int damage = 3;
    public GameObject projectilePrefab; // 2D projectile prefab (Rigidbody2D + Collider2D)
}
