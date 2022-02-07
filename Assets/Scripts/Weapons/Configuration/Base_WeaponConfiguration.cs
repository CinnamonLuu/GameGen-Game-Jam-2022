using UnityEngine;

public class Base_WeaponConfiguration : ScriptableObject
{
    [SerializeField]
    private Stat damage;
    [SerializeField]
    private Stat attackSpeed;

    public Upgrade[] upgrades;

    public virtual Base_WeaponBehaviour InstantiateWeaponBehaviour()
    {
        return null;
    }

    public float GetDamageAmount()
    {
        //add updates
        return damage.amount;
    }
    
    public float GetAttackSpeedAmount()
    {
        //add updates
        return attackSpeed.amount;
    }
}
