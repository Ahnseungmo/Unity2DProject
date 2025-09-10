using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    private WeaponInstance weapon;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Init(WeaponInstance weapon)
    {
        this.weapon = weapon;
    }

    public void Launch(Vector2 force)
    {
        rb.AddForce(force, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Target target = collision.gameObject.GetComponent<Target>();
        if (target != null)
        {
            GameManager.Instance.DamageMonster(target.monsterId, weapon.GetDamage());
        }
        Destroy(gameObject);
    }
}
