using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class StrongEnemy : Enemy
{
    private GameObject player;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float ranSpeed = 6f;
    // в≥дстань на к≥й ворог починаЇ пересл≥дувати гравц€
    [SerializeField] private float chaseRange = 10f;
    [SerializeField] private float stopChaseRange = 15f;
    [SerializeField] private int attackDamage = 10;
    [SerializeField] private float attackRange = 2f;
    private float distanceToPlayer;
    private Vector3 startPosition;
    private bool isAttacking;

    protected override void Start()
    {
        base.Start();
        player = FindFirstObjectByType("PlayerController");
        // збер≥гаю початкову позиц≥ю ворога
        startPosition = transform.position;
    }

    void Update()
    {
        // розраховуЇмо в≥дстань в≥д ворога до гравц€
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < chaseRange)
        {
            ChasePlayer();
        }
        else if (distanceToPlayer > stopChaseRange)
        {
            RunRandomDirection();
        // //GoToStartPosition();
        //}
        //else
        //{
        //    StopMoving();
        //    //RunRandomDirection();
        }
    }

    // пересл≥дуванн€ гравц€
    private void ChasePlayer()
    {
        // визначаЇмо напр€мок руху до гравц€
        Vector3 direction = (player.transform.position - transform.position).normalized;
        if(distanceToPlayer < attackRange && isAttacking)
        {
            StartCoroutine(Attack());
        }
        else if(distanceToPlayer > attackRange)
        {
            MoveTowardsPlayer();
        }
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        while (player != null && distanceToPlayer < attackRange)
        {
            player.TakeDamage();
            yield return new WaitForSeconds(1f);
        }
        isAttacking = false;
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = (player.transform.position - transform.position);
        transform.position += direction * moveSpeed * Time.deltaTime;
        transform.LookAt(player.transform);
    }

    void RunRandomDirection()
    {
        Vector3 randomDirection = new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1)).normalized;
        transform.position += randomDirection * moveSpeed * Time.deltaTime;
        transform.LookAt(transform.position + randomDirection);
    }

    
}
