using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    protected int currentHealth;

    public float moveSpeed = 5f;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        // включити ан≥мац≥ю ≥ звук
        Destroy(gameObject);
    }

    protected virtual void Move()
    {
        // рух енем≥ до гравц€, чи в ≥нших напр€мках
    }
}
