using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RangedWeapon", menuName = "ScriptableObjects/Ranged Weapon")]
public class Ranged_WeaponConfiguration : Base_WeaponConfiguration
{
    public float projectileSpeed;
    public float numberOfProjectile;

    public Ranged_WeaponBehaviour InstantiateRangedWeaponBehaviour()
    {
        return null;
    }
}
