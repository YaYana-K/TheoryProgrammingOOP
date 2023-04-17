using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class StrongEnemy : Enemy
{
    // ������� �� �� ����� ������ ������������ ������
    [SerializeField] private float chaseRange = 10f;
    [SerializeField] private float stopChaseRange = 15f;

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        DistanceToPlayer();
        Move();

        if (distanceToPlayer < chaseRange && player != null)
        {
            ChasePlayer();
        }
    }

    // ������������� ������
    private void ChasePlayer()
    {
        // ��������� �������� ���� �� ������
        Vector3 direction = (player.transform.position - transform.position).normalized;
        if(distanceToPlayer < attackRange && !isAttacking)
        {
            StartCoroutine(Attack());
        }
        else if(distanceToPlayer > attackRange)
        {
            MoveTowardsPlayer();
        }
    }

   

    private void MoveTowardsPlayer()
    {
        Vector3 direction = (player.transform.position - transform.position);
        transform.position += direction * moveSpeed * Time.deltaTime;
        transform.LookAt(player.transform);
    }
}
