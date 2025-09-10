public class WeaponInstance
{
    public WeaponData Data;
    public bool Used;

    public WeaponInstance(WeaponData data)
    {
        Data = data;
        Used = false;
    }

    public int GetDamage()
    {
        return Data.Damage;
    }
}
