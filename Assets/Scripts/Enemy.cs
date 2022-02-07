using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    Stat health;

    public void TakeDamage(float damageAmount)
    {
        health.amount -= damageAmount;
        if (health.amount <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        //drop object random
        Destroy(this);
    }
}
