using UnityEngine;

public class TurretSettings : MonoBehaviour
{
	[SerializeField] private GameObject turretMount;
	private CharacterController characterController;
	private Damageable damageable;
	private AutomaticWeapon weapon;
	private void Awake()
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
}
