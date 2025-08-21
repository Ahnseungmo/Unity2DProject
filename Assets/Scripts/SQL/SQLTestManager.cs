using System.Collections.Generic;
using UnityEngine;

public class SQLTestManager : MonoBehaviour
{
    private SQLiteManager _db;

    void Start()
    {
        _db = new SQLiteManager();
        _db.Init("data.db");

        // 캐릭터 추가
        var newChar = new Character
        {
            Name = "Knight",
            HP = 200,

        };
        _db.InsertCharacter(newChar);

        // 전체 캐릭터 출력
        List<Character> characters = _db.GetAllCharacters();
        foreach (var c in characters)
        {
            Debug.Log($"[{c.CharacterId}] {c.Name} / HP: {c.HP} / ");
        }

        // 특정 캐릭터 수정
        var first = characters[0];
        first.HP += 50;
        _db.UpdateCharacter(first);

        // 삭제 테스트
        //_db.DeleteCharacter(first.Id);
    }
}
