using UnityEngine;

public class BattleUI : MonoBehaviour
{
    public static BattleUI Instance;
    void Awake() => Instance = this;

    public Transform projectileSpawnPointB; // B 화면 발사 위치
    public GameObject projectilePrefab;

    public void ShowNextWeapon(WeaponTemplate weapon)
    {
        // B 화면에서 발사체 생성
        GameObject go = Instantiate(projectilePrefab, projectileSpawnPointB.position, Quaternion.identity);
        go.GetComponent<WeaponProjectile>().data = weapon;
    }
}