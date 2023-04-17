using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BasicEnemy : Enemy
{
    public float fleeDistance = 20f;
    public float chaseDistance = 10f;

    private bool isPlayerNearby = false;
    private bool isOtherEnemyNearby = false;
    private Vector3 fleeDirection;
    private List<BasicEnemy> nearbyEnemies = new List<BasicEnemy>();
    

    protected override void Start()
    {
        base.Start();
        
        // ��������� ����������� ����� �� ����
        //transform.position = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
    }

    void Update()
    {
        DistanceToPlayer();
        // ���� ������� �� �����, �� ���� �������� ���������� �����
        Move();

        if (player != null)
        {
            // ����������, �� ����������� ������� ����� � ����
            CheckForPlayer();

            // ����������, �� ����������� ����� ���� ���� � ���� ����� � ����
            CheckForNearbyEnemies();

            if (isPlayerNearby)
            {
                if (!isOtherEnemyNearby)
                {
                    // ���� ����� ���� ����� ����� ���� � ����, �� ���� ���� �� ������
                    FleeFromPlayer();
                }
                else
                {
                    // ���� ����� � ���� ���� ���� � ����, �� �� ���� ��������� �� ������
                    ChasePlayerWithOthers();
                }
            }
        }
           
    }

    private void CheckForPlayer()
    {
        // ����������, �� ����������� ������� � ����� fleeDistance �� ����
        if (distanceToPlayer < fleeDistance)
        {
            isPlayerNearby = true;
        }
        else if (distanceToPlayer > chaseDistance)
        {
            isPlayerNearby = false;
        }
    }

    private void CheckForNearbyEnemies()
    {
        // ����������, �� ����������� ����� ���� ���� � ���� � ����� fleeDistance �� ����
        nearbyEnemies.Clear();
        BasicEnemy[] enemies = FindObjectsOfType<BasicEnemy>();
        foreach (BasicEnemy enemy in enemies)
        {
            if (enemy != this && Vector3.Distance(transform.position, enemy.transform.position) < fleeDistance)
            {
                nearbyEnemies.Add(enemy);
            }
        }

        isOtherEnemyNearby = nearbyEnemies.Count > 0;
    }

    private void FleeFromPlayer()
    {
        fleeDirection = (transform.position - player.transform.position).normalized;
        fleeDirection.y = 0f;
        transform.position += fleeDirection * runSpeed * Time.deltaTime;
    }

    
    void ChasePlayerWithOthers()
    {
        Vector3 direction = player.transform.position - transform.position;
        direction.y = 0f;
        direction.Normalize();

        transform.Translate(direction * runSpeed * Time.deltaTime, Space.World);
        StartCoroutine(Attack());
    }

}
