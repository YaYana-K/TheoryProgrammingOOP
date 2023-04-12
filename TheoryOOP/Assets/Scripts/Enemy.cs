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
        // �������� ������� � ����
        Destroy(gameObject);
    }

    protected virtual void Move()
    {
        // ��� ���� �� ������, �� � ����� ���������
    }
}
