using UnityEngine;

public class Player : Character
{
    // (선택) 플레이어 전용 참조들
    // public Transform handPoint; // 플레이어 애니에서 투사체 스폰 타이밍을 맞출 때 사용 가능

    public override void Die()
    {
        animator?.SetTrigger("Die");
        Debug.Log("Player died - Game Over (여기서 게임오버 처리)");
    }

    // 공격 애니메이션(던지기) 재생
    public void PlayThrowAnimation()
    {
        animator?.SetTrigger("Throw");
    }
}
