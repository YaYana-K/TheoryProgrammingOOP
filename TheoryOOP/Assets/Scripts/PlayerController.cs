using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 5f;
    public float attackRange = 1f; // дальність атаки плеєра
    public int attackDamage = 1;
    public float attackDelay = 1f; // затримка між атаками плеєра
    public int health;

    private Animator animator;
    private bool isAttacking;
    private float lastAttackTime;
    void Start()
    {
        animator = GetComponent<Animator>();
        isAttacking = false;
        lastAttackTime = -attackDelay;
    }

    // Update is called once per frame
    void Update()
    {
        // рух плеєра
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed;
        transform.Translate(movement * Time.deltaTime, Space.World);

        // поворот плеєра
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up, mouseX * rotateSpeed);

        // атака плеєра
        if(Input.GetMouseButton(0) && !isAttacking && Time.time - lastAttackTime >= attackDelay)
        {
            isAttacking=true;
            animator.SetTrigger("attack");
            lastAttackTime= Time.time;
            Attack();
        }
    }

    private void Attack()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);
        foreach(Collider collider in hitColliders)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if(enemy != null) 
            {
                enemy.TakeDamage(attackDamage);
            }
        }
    }

    void EndAttack()
    {
        isAttacking = false;
    }

     public void TakeDamage(int attackDamege)
    {
        health -= attackDamage;
        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {

    }
}
