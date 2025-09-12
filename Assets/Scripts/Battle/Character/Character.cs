using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [Header("Stats")]
    public string characterId;
    public int maxHp = 10;
    public int currentHp = 10;
    public int attack = 1;

    [Header("Animation")]
    public Animator animator;

    public bool IsAlive => currentHp > 0;

    // A/B 화면 동기화를 위해 연결 참조 (Monster 타입)
    [HideInInspector]
    public Monster linkedMonster;

    public virtual void Init(string id, int hp, int atk)
    {
        characterId = id;
        maxHp = hp;
        currentHp = hp;
        attack = atk;
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    public virtual void TakeDamage(int dmg)
    {
        if (!IsAlive) return;
        currentHp -= dmg;
        animator?.SetTrigger("3_Damaged");
        if (currentHp <= 0)
        {
            currentHp = 0;
            Die();
        }
    }

    public abstract void Die();
}
