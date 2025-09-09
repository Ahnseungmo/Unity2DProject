using JetBrains.Annotations;
using Unity.VisualScripting.Dependencies.Sqlite;



public class DBItem
{
    [PrimaryKey, AutoIncrement]
    public int ItemID {  get; set; }
    public int ItemTemplateID { get; set; }
 

}
