using UnityEngine;
using UnityEngine.AI;

public class ZombieBase : MonoBehaviour
{
    [Header("Base Stats")]
    public float moveSpeed = 3.5f;
    public float maxHealth = 100f;
    public float attackDamage = 10f;
    public float attackRange = 2f;
    public float attackCooldown = 1f;

    protected Transform player;
    protected NavMeshAgent agent;
    protected float currentHealth;
    protected float lastAttackTime;

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        currentHealth = maxHealth;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj.transform;
    }

    protected virtual void Update()
    {
        agent.SetDestination(player.position);

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            TryAttack();
        }
    }

    protected virtual void TryAttack()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;

            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }
        }
    }

    public virtual void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        GameManager.instance.AddScore(10);
        GameManager.instance.OnZombieKilled();
        Destroy(gameObject);
    }
}