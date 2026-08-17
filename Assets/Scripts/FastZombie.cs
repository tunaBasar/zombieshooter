public class FastZombie : ZombieBase
{
    protected override void Start()
    {
        base.Start();

        moveSpeed = 6f;
        maxHealth = 50f;
        attackDamage = 5f;

        agent.speed = moveSpeed;
        currentHealth = maxHealth;
    }
}