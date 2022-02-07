using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    Health,
    Damage,
    AttackSpeed,
    ProjectileNumber,
    MovementSpeed,
    Range
}
[System.Serializable]
public class Stat
{
    public StatType type;
    public float amount;
}
