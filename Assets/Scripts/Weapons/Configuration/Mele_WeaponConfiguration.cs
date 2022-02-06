using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MeleeWeapon", menuName = "ScriptableObjects/Melee Weapon")]
public class Mele_WeaponConfiguration : Base_WeaponConfiguration
{
    public Mele_WeaponBehaviour InstantiateMeleWeaponBehaviour() 
    {
        return null;
    }
}
