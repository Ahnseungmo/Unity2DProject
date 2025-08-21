using System.Collections.Generic;
using UnityEngine;

public class SQLTestManager : MonoBehaviour
{
    private SQLiteManager _db;

    void Start()
    {
        _db = new SQLiteManager();
        _db.Init("data.db");

        // ĳ���� �߰�
        var newChar = new Character
        {
            Name = "Knight",
            HP = 200,

        };
        _db.InsertCharacter(newChar);

        // ��ü ĳ���� ���
        List<Character> characters = _db.GetAllCharacters();
        foreach (var c in characters)
        {
            Debug.Log($"[{c.CharacterId}] {c.Name} / HP: {c.HP} / ");
        }

        // Ư�� ĳ���� ����
        var first = characters[0];
        first.HP += 50;
        _db.UpdateCharacter(first);

        // ���� �׽�Ʈ
        //_db.DeleteCharacter(first.Id);
    }
}
