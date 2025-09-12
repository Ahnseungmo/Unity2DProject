using System.Collections;
using UnityEngine;

public class Monster : Character
{
    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public override void Die()
    {
        animator?.SetTrigger("Die");
        // 단순히 비활성화 처리 (원하면 풀 페이드나 슬롯 제거 처리)
        gameObject.SetActive(false);
    }

    // TakeDamage는 Character.TakeDamage 로직을 확장해서
    // A/B 양쪽을 동기화하고 피격 이펙트를 처리
    public override void TakeDamage(int dmg)
    {
        if (!IsAlive) return;

        currentHp -= dmg;
        animator?.SetTrigger("Hit");

        // 시각적 피격 효과
        StartCoroutine(HitEffect());

        // A/B 동기화: linkedMonster가 있으면 체력값과 피격 애니메이션을 동기화
        if (linkedMonster != null && linkedMonster != this)
        {
            linkedMonster.SyncFromLinked(currentHp);
        }

        if (currentHp <= 0)
        {
            currentHp = 0;
            Die();
        }
    }

    // linked 쪽에서 현재 체력으로 동기화할 때 사용
    public void SyncFromLinked(int hp)
    {
        currentHp = hp;
        animator?.SetTrigger("Hit");
        if (currentHp <= 0) Die();
    }

    IEnumerator HitEffect()
    {
        if (sr != null)
        {
            sr.color = Color.red;
            yield return new WaitForSeconds(0.12f);
            sr.color = Color.white;
        }
    }

    // 몬스터가 플레이어 공격 시 호출
    public void DoAttack(Player player)
    {
        if (!IsAlive) return;
        animator?.SetTrigger("Attack");
        player.TakeDamage(attack);
    }
}
