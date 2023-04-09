public class ActorHealth
{
	private readonly float maxHealth = 100.0f;
	public float Health { get; private set; }
	public ActorHealth()
	{
		Health = maxHealth;
	}

	public void TakeDamage(float damage)
	{
		Health -= damage;

		if (Health <= 0)
		{
			Health = 0;
		}
	}

	public bool IsDead => Health <= 0;
}
