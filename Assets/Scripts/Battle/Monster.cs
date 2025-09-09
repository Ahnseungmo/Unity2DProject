using UnityEngine;

public class Monster : Character
{
    public int monsterId;

    // 움직임 패턴
    public float speed = 2f;
    public float range = 3f;
    private Vector3 startPos;
    private bool movingRight = true;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        MovePattern();
    }

    void MovePattern()
    {
        if (movingRight)
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        else
            transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (Vector2.Distance(startPos, transform.position) >= range)
            movingRight = !movingRight;
    }

    // 턴 시 공격 함수
    public int Attack()
    {
        int damage = Random.Range(5, 15);
        Debug.Log($"{characterName}이 플레이어에게 {damage} 피해");
        return damage;
    }
}
