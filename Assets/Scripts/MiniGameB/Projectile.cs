using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 10;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TargetMonster target = collision.gameObject.GetComponent<TargetMonster>();
        if (target != null)
        {
            target.Hit(damage);
        }
        Destroy(gameObject);
    }
}