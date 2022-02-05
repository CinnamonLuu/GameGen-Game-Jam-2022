using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapon")]
public class Base_weapon : ScriptableObject
{
    enum WeaponType
    {
        Melee, 
        Ranged,
        Area
    }
    [SerializeField]
    WeaponType m_type;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
