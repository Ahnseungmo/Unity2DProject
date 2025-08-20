
using Unity.VisualScripting.Dependencies.Sqlite;

public class Character
{
    [PrimaryKey, AutoIncrement]
    public int CharacterId { get; set; }

    [MaxLength(100)]
    public string Name { get; set; }

    public int Level { get; set; } = 1;
    public int EXP { get; set; } = 0;
    public int HP { get; set; } = 100;
    public int MP { get; set; } = 100;
    public int STR { get; set; } = 4;
    public int DEX { get; set; } = 4;
    public int INT { get; set; } = 4;
    public int LUK { get; set; } = 4;
    public int StatPoint { get; set; } = 5;

}