public class TankZombie : ZombieBase
{
    protected override void Start()
    {
        base.Start();

        moveSpeed = 1.5f;
        maxHealth = 250f;
        attackDamage = 20f;

        agent.speed = moveSpeed;
        currentHealth = maxHealth;
    }
}