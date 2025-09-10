using UnityEngine;

public class SlingshotController : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float power = 5f;
    public float maxDrag = 3f;

    private Vector2 dragStartPos;
    private bool isDragging = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            dragStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            Vector2 dragEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 force = (dragStartPos - dragEndPos) * power;

            WeaponInstance weapon = PlayerInventory.Instance.GetCurrentWeapon();
            if (weapon != null && !weapon.Used)
            {
                SpawnProjectile(weapon, force);
                weapon.Used = true;
            }

            isDragging = false;
        }
    }

    void SpawnProjectile(WeaponInstance weapon, Vector2 force)
    {
        GameObject projObj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Projectile projectile = projObj.GetComponent<Projectile>();
        projectile.Init(weapon);
        projectile.Launch(force);
    }
}
