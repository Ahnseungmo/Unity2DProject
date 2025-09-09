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
    /// DB 파일 경로 및 연결 초기화
    /// </summary>
    public void Init(string fileName)
    {

        string dbPath = Path.Combine(Path.GetFullPath(Application.streamingAssetsPath),"Tables", fileName);
        Debug.Log("DB Path: " + dbPath);
        _connection = new SQLiteConnection(dbPath);

        // 테이블 생성 (존재하지 않으면 자동 생성)
        _connection.CreateTable<DBCharacter>();
    }

    public void InsertCharacter(DBCharacter character)
    {
        _connection.Insert(character);
    }

    public List<DBCharacter> GetAllCharacters()
    {
        return _connection.Table<DBCharacter>().ToList<DBCharacter>();
    }

    public DBCharacter GetCharacterById(int id)
    {
        return _connection.Find<DBCharacter>(id);
    }

    public void UpdateCharacter(DBCharacter character)
    {
        _connection.Update(character);
    }

    public void DeleteCharacter(int id)
    {
        _connection.Delete<DBCharacter>(id);
    }
}