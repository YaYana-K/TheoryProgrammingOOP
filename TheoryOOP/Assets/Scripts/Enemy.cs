using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected int maxHealth = 100;
    [SerializeField]protected int currentHealth;
    protected GameObject player;
    [SerializeField]protected float moveSpeed = 5f;
    [SerializeField] protected float runSpeed;
    [SerializeField] protected int damage;
    [SerializeField] protected float attackRange = 2f;
    protected float currentSpeed;
    protected float timeToChangeDirection;
    protected float minTimeToChangeDirection = 3, maxTimeToChangeDirection = 10;
    protected bool isAttacking;
    protected float distanceToPlayer;
    private PlayerController playerController;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = FindObjectOfType<PlayerController>();
    }
    protected IEnumerator Attack()
    {
        isAttacking = true;
        while (player != null && distanceToPlayer < attackRange)
        {
            playerController.TakeDamage(damage);
            Debug.Log("Damage Player " + damage);
            yield return new WaitForSeconds(1f);
        }
        isAttacking = false;
    }

    protected float DistanceToPlayer()
    {
        if (player != null)
        {
            // розраховуЇмо в≥дстань в≥д ворога до гравц€
            distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        }
        return distanceToPlayer;
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
}
