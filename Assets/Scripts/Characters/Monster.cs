using UnityEngine;

public class Monster : Character
{
    public int attackDamage = 10;

    public void AttackPlayer()
    {
        Player player = FindFirstObjectByType<Player>();
        if (player != null)
        {
            player.TakeDamage(attackDamage);
        }
    }
}