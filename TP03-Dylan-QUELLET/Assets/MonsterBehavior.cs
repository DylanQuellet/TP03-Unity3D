using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehavior : MonoBehaviour
{
    public Transform player;
    public PlayerNew PlayerNew;
    public Animator animator;
    public float trackingRange = 50f;
    private bool isPlayerInRange = false;
    public float moveSpeed = 2f;

    public int maxHealth = 100;
    public int currentHealth;
    public int attackPower = 10;
    public HealthBar healthBar;

    private float nextAttackTime = 0f;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        animator = GetComponent<Animator>();
        animator.SetBool("Alive", true);
    }

    void Update()
    {
        float distancePlayer = Vector3.Distance(player.position, transform.position);

        if (distancePlayer <= trackingRange && currentHealth > 0)
        {
            LookToPlayer();

            if (distancePlayer <= 5f)
            {
                animator.SetBool("Attacking", true);
                if (Time.time > nextAttackTime)
                {
                    //TakeDamage(attackPower * 2);
                    PlayerNew.TakeDamage(attackPower);
                    nextAttackTime = Time.time + 1f;
                }
            }
            else
            {
                animator.SetBool("Walking", true);
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
                animator.SetBool("Attacking", false);
            }
        }
        else
        {
            animator.SetBool("Attacking", false);
            animator.SetBool("Walking", false);

        }

    }

    void LookToPlayer()
    {
        
        Vector3 direction = (player.position - transform.position).normalized;

        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Gérer la mort du monstre
        Debug.Log(gameObject.name + " est mort !");
        animator.SetBool("Alive", false);

    }
}
