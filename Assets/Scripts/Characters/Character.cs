using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public string characterName;
    public int hp;

    public virtual void TakeDamage(int amount)
    {
        hp -= amount;
        if (hp <= 0) Die();
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}