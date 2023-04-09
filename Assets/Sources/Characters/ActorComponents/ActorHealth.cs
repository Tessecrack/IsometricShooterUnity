using UnityEngine;

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
		Health = Mathf.Clamp(Health - damage, 0, maxHealth);
	}

	public bool IsDead => Health <= 0;
}
