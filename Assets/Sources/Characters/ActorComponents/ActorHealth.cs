using UnityEngine;

public class ActorHealth
{
	private readonly int maxHealth = 100;
	public int Health { get; private set; }
	public ActorHealth()
	{
		Health = maxHealth;
	}

	public void TakeDamage(int damage)
	{
		Health = Mathf.Clamp(Health - damage, 0, maxHealth);
	}

	public bool IsDead => Health <= 0;
}
