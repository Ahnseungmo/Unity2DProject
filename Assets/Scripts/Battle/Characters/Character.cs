using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public string characterName;
    public int maxHp;
    public int currentHp;

    public bool IsDead => currentHp <= 0;

    public virtual void TakeDamage(int damage)
    {
        currentHp -= damage;
        if (currentHp < 0) currentHp = 0;
    }

    public virtual void Heal(int amount)
    {
        currentHp += amount;
        if (currentHp > maxHp) currentHp = maxHp;
    }
}
