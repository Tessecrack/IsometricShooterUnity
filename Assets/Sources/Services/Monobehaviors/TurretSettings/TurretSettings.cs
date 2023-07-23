using UnityEngine;

public class TurretSettings : MonoBehaviour
{
	[SerializeField] private GameObject turretMount;
	[SerializeField] private int maxHealth = 100;
	[SerializeField] private float rangeDetection = 8.0f;
	private CharacterController characterController;
	private Damageable damageable;
	private AutomaticWeapon weapon;
	public void Init()
	{
		characterController = this.turretMount.GetComponent<CharacterController>();
		damageable = this.turretMount.GetComponent<Damageable>();
		weapon = this.turretMount.GetComponent<AutomaticWeapon>();
	}

	public GameObject GetTurretMount() => turretMount;
	public CharacterController GetCharacterController() => characterController;
	public Damageable GetDamageable() => damageable;
	public Transform GetTransform() => turretMount.transform;
	public Weapon GetWeapon() => weapon;
	public int GetMaxHealth() => maxHealth;

	public float RangeDetection => rangeDetection;
}
