using UnityEngine;

public class Damageable : MonoBehaviour
{
	public bool IsTakedDamage { get; private set; }
	public bool IsDeath { get; private set; }

	private int damage;

	public void TakeDamage(int damage)
	{
		IsTakedDamage = true;
		this.damage = damage;
	}
	public int ApplyDamage(int currentHealth)
	{
		int result = currentHealth - damage;
		if (result <= 0)
		{
			result = 0;
			IsDeath = true;
		}
		Reset();
		return result;
	}

	public void DisableObject()
	{
		Destroy(gameObject);
	}

	private void Reset()
	{
		IsTakedDamage = false;
		this.damage = 0;
	}
}
