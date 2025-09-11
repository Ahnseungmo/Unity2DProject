using UnityEngine;

public class TargetMonster : MonoBehaviour
{
    public Monster linkedMonster;

    public void Hit(int damage)
    {
        if (linkedMonster != null)
        {
            linkedMonster.TakeDamage(damage);
        }
    }
}