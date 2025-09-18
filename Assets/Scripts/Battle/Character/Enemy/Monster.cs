using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

public class Monster : Character
{
    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public override void Die()
    {
        animator?.SetTrigger("4_Death");
        // 단순히 비활성화 처리 (원하면 풀 페이드나 슬롯 제거 처리)
        //        animator?.GetBool();

        SplineAnimate spani = GetComponent<SplineAnimate>();
        if (spani != null) {
            spani.enabled = false;
        }
        StartCoroutine(Death());

    }



    // TakeDamage는 Character.TakeDamage 로직을 확장해서
    // A/B 양쪽을 동기화하고 피격 이펙트를 처리
    public override void TakeDamage(int dmg)
    {
        if (!IsAlive) return;

        currentHp -= dmg;
        animator?.SetTrigger("3_Damaged");

        // 시각적 피격 효과
        StartCoroutine(HitEffect());

        // A/B 동기화: linkedMonster가 있으면 체력값과 피격 애니메이션을 동기화
        if (linkedMonster != null && linkedMonster != this)
        {
            linkedMonster.SyncFromLinked(currentHp, dmg);
        }

        if (currentHp <= 0)
        {
            currentHp = 0;
            Die();
        }
    }

    // linked 쪽에서 현재 체력으로 동기화할 때 사용
    public void SyncFromLinked(int hp,int dmg = 0)
    {
        currentHp = hp;
        animator?.SetTrigger("3_Damaged");
        print(dmg);
        DamageFont damage = PoolingManager.Get.SpawnPool("Damage",gameObject.transform).GetComponent<DamageFont>();
        damage.PrintDamage(dmg);

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
    IEnumerator Death()
    {
        yield return null;

        // 3. 현재 애니메이션 상태 정보 가져오기
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // 4. 현재 재생 중인 애니메이션의 길이 가져오기
        float animationLength = stateInfo.length;

        // 5. 해당 시간만큼 대기
        yield return new WaitForSeconds(animationLength);
        gameObject.SetActive(false);

    }

    // 몬스터가 플레이어 공격 시 호출
    public void DoAttack(Player player)
    {
        if (!IsAlive) return;
        animator?.SetTrigger("2_Attack");
        StartCoroutine(Attack(player));

    }
    IEnumerator Attack(Player player)
    {
        yield return null;

        // 3. 현재 애니메이션 상태 정보 가져오기
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // 4. 현재 재생 중인 애니메이션의 길이 가져오기
        float animationLength = stateInfo.length;

        // 5. 해당 시간만큼 대기
        yield return new WaitForSeconds(animationLength);

        player.TakeDamage(attack);
    }

}