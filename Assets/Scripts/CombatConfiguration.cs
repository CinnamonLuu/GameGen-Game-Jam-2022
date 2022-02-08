using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatConfiguration : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField]
    public  Stat health;
    [SerializeField]
    public Stat range;
    [SerializeField]
    public Stat attackSpeed;

    private float elapsed;
    public float Elapsed { get => elapsed; set => elapsed = value; }

    [Header("Weapon")]
    [SerializeField]
    private Base_WeaponConfiguration m_weaponConfiguration;

    private Base_WeaponBehaviour m_weapon;
    public Base_WeaponConfiguration WeaponConfiguration { get => m_weaponConfiguration; set => m_weaponConfiguration = value; }
    public Base_WeaponBehaviour Weapon { get => m_weapon; set => m_weapon = value; }


    public Animator animator;



    public Vector2 characterForward;

    public LayerMask target;


    // Start is called before the first frame update
    void Start()
    {
        switch (gameObject.layer)
        {
            case 6:
                target = 8;
                break;
            case 8:
                target = 6;
                break;
        }

        Debug.Log(target);
        Weapon = WeaponConfiguration.InstantiateWeaponBehaviour();
        Weapon.SetCombatConfiguration(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void TakeDamage(float damageAmount)
    {
        health.amount -= damageAmount;
        animator.SetBool("Damage", true);
        if (health.amount <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        //drop object random
        Destroy(gameObject);
    }
}
