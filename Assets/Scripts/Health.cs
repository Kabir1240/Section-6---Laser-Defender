using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 50;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        if (damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
        }
    }

    void TakeDamage(int damageTaken)
    {
        health -= damageTaken;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
