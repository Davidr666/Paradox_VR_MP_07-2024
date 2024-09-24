using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    public float attackRange = 1.5f;
    public int damageToPlayer = 10;
    public int maxHealth = 50;
    public int currentHealth;

    private Transform player;
    private GameManager gameManager; // Referencia al GameManager
    private float attackCooldown = 2f;
    private float attackTimer = 0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gameManager = GameObject.FindObjectOfType<GameManager>(); // Encuentra el GameManager en la escena
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= attackRange)
            {
                AttackPlayer();
            }
            else
            {
                ChasePlayer();
            }

            attackTimer += Time.deltaTime;
        }
    }

    void ChasePlayer()
    {
        if (player != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
        }
    }

    void AttackPlayer()
    {
        if (attackTimer >= attackCooldown && gameManager != null)
        {
            gameManager.playerHealth -= damageToPlayer;
            Debug.Log("Player Health: " + gameManager.playerHealth);
            attackTimer = 0f;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Acciones al morir, por ejemplo, destruir el objeto o reproducir una animación
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        // Dibuja el rango de ataque del enemigo en la escena para facilitar el ajuste
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
