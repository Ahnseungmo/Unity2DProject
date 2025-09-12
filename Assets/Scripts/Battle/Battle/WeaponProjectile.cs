using System.Collections;
using UnityEngine;

public class WeaponProjectile : MonoBehaviour
{
    public WeaponTemplate data;
    private bool stopped = false;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision col)
    {
        Monster monsterB = col.gameObject.GetComponent<Monster>();
        if (monsterB != null)
        {
            // B 화면 몬스터 피격
            monsterB.TakeDamage(data.damage);

            // A 화면 몬스터에게도 동일하게 반영
            if (monsterB.linkedMonster != null)
            {
                monsterB.linkedMonster.TakeDamage(data.damage);
            }
        }
    }

    void Update()
    {
        if (!stopped && rb.linearVelocity.magnitude < 0.1f)
        {
            stopped = true;
            StartCoroutine(WaitAndReadyNext());
        }
    }

    IEnumerator WaitAndReadyNext()
    {
        yield return new WaitForSeconds(1f);
        BattleManager.Instance.ReadyNextShot();
    }
}
