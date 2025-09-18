using UnityEngine;

[CreateAssetMenu(menuName = "Battle/MonsterTemplate")]
public class MonsterTemplate : ScriptableObject
{
    public string name;
    public GameObject projectilePrefab; // 2D projectile prefab (Rigidbody2D + Collider2D)
}
