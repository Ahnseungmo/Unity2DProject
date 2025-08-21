using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class SQLiteManager
{
    private SQLiteConnection _connection;

    /// <summary>
    /// DB ���� ��� �� ���� �ʱ�ȭ
    /// </summary>
    public void Init(string fileName)
    {

        string dbPath = Path.Combine(Path.GetFullPath(Application.streamingAssetsPath),"Tables", fileName);
        Debug.Log("DB Path: " + dbPath);
        _connection = new SQLiteConnection(dbPath);

        // ���̺� ���� (�������� ������ �ڵ� ����)
        _connection.CreateTable<Character>();
    }

    public void InsertCharacter(Character character)
    {
        _connection.Insert(character);
    }

    public List<Character> GetAllCharacters()
    {
        return _connection.Table<Character>().ToList<Character>();
    }

    public Character GetCharacterById(int id)
    {
        return _connection.Find<Character>(id);
    }

    public void UpdateCharacter(Character character)
    {
        _connection.Update(character);
    }

    public void DeleteCharacter(int id)
    {
        _connection.Delete<Character>(id);
    }
}