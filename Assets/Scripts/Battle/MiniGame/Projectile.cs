using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
    Rigidbody2D rb;
    WeaponTemplate weapon;
    bool launched = false;
    bool ended = false;

    // 멈춤 감지 임계치
    public float stopVelocityThreshold = 0.15f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    // power: scalar, direction: normalized
    public void Init(WeaponTemplate w, float power, Vector2 direction)
    {
        weapon = w;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.linearVelocity = direction * power;
        launched = true;
        // 안전 삭제
        Destroy(gameObject, 6f);
    }

    private void Update()
    {
        if (!launched || ended) return;

        if (rb.linearVelocity.magnitude < stopVelocityThreshold)
        {
            EndShot();
        }
    }

    private void EndShot()
    {
        if (ended) return;
        ended = true;
        StartCoroutine(NotifyAndDestroy());
    }

    IEnumerator NotifyAndDestroy()
    {
        // 멈춘 시점에 BattleManager에게 알림해서 1초 대기 후 다음샷 준비하게 함
        BattleManager.Instance.OnProjectileStopped();
        // 약간 딜레이를 두고 파괴 (없어도 됨)
        yield return new WaitForSeconds(0.1f);
        print("무기 파괴");
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ended) return; // 이미 처리된 경우 무시

        var monster = collision.gameObject.GetComponent<Monster>();
        print(monster);
        if (monster != null && weapon != null)
        {
            monster.TakeDamage(weapon.damage);
            print(monster.currentHp);
        }

        // 충돌도 멈춘 것으로 간주
        EndShot();
    }
}
