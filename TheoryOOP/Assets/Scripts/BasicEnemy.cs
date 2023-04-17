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
        
        // випадково розташовуємо енемів на сцені
        //transform.position = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
    }

    void Update()
    {
        DistanceToPlayer();
        // якщо гравець не поруч, то енемі рухається випадковим чином
        Move();

        if (player != null)
        {
            // перевіряємо, чи знаходиться гравець поруч з енемі
            CheckForPlayer();

            // перевіряємо, чи знаходиться інший енемі того ж типу поруч з енемі
            CheckForNearbyEnemies();

            if (isPlayerNearby)
            {
                if (!isOtherEnemyNearby)
                {
                    // якщо поряд немає інших енемів того ж типу, то енемі втікає від гравця
                    FleeFromPlayer();
                }
                else
                {
                    // якщо поряд є інші енемі того ж типу, то всі вони нападають на гравця
                    ChasePlayerWithOthers();
                }
            }
        }
           
    }

    private void CheckForPlayer()
    {
        // перевіряємо, чи знаходиться гравець в межах fleeDistance від енемі
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
        // перевіряємо, чи знаходиться інший енемі того ж типу в межах fleeDistance від енемі
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
