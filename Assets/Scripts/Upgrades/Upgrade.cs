using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : ScriptableObject
{
    public enum UpgradeType
    {
        Attack,
        AttackSpeed,
        ProjectileNumber,
        Life
    }

    public UpgradeType upgradeType;
    public bool sum;
    public int amount;
}
