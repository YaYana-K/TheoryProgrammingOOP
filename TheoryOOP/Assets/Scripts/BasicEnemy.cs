using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BasicEnemy : Enemy
{
    public float damage = 10f;
    public float fleeDistance = 20f;
    public float chaseDistance = 10f;
    public float runSpeed = 2;

    private bool isPlayerNearby = false;
    private bool isOtherEnemyNearby = false;
    private Vector3 fleeDirection;
    private List<BasicEnemy> nearbyEnemies = new List<BasicEnemy>();
    private GameObject player;
    private float currentSpeed;
    
    private float timeToChangeDirection;
    private float minTimeToChangeDirection = 3, maxTimeToChangeDirection = 10;

    protected override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player");
        // ��������� ����������� ����� �� ����
        //transform.position = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
    }

    void Update()
    {
        //EnableDisableEnemy();

        if(isActiveAndEnabled)
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
            else
            {
                // ���� ������� �� �����, �� ���� �������� ���������� �����
                Move();
            }
        }
        
    }

    private void CheckForPlayer()
    {
        // ����������, �� ����������� ������� � ����� fleeDistance �� ����
        if (Vector3.Distance(transform.position, player.transform.position) < fleeDistance)
        {
            isPlayerNearby = true;
        }
        else if (Vector3.Distance(transform.position, player.transform.position) > chaseDistance)
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

    protected override void Move()
    {
        if (currentSpeed == 0f)
        {
            currentSpeed = moveSpeed;
            timeToChangeDirection = Random.Range(minTimeToChangeDirection, maxTimeToChangeDirection);
        }

        timeToChangeDirection -= Time.deltaTime;
        if (timeToChangeDirection < 0f)
        {
            Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
            transform.rotation = Quaternion.LookRotation(randomDirection);
            timeToChangeDirection = Random.Range(minTimeToChangeDirection, maxTimeToChangeDirection);
        }

        transform.position += transform.forward * currentSpeed * Time.deltaTime;
    }
    void ChasePlayerWithOthers()
    {
        Vector3 direction = player.transform.position - transform.position;
        direction.y = 0f;
        direction.Normalize();

        transform.Translate(direction * runSpeed * Time.deltaTime, Space.World);
        //player.TakeDamage(damage);
    }
    //private void EnableDisableEnemy()
    //{
    //    float distance = Vector3.Distance(transform.position, player.transform.position);
    //    if (distance < 100)
    //    {
    //        gameObject.SetActive(true);
    //    }
    //    else
    //    {
    //        gameObject.SetActive(false);
    //    }
    //}
}
