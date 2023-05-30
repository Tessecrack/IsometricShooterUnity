using UnityEngine;

public class TurretSettings : MonoBehaviour
{
	[SerializeField] private GameObject turretMount;
	private CharacterController characterController;
	private Damageable damageable;
	private void Awake()
	{
		characterController = this.turretMount.GetComponent<CharacterController>();
		damageable = this.turretMount.GetComponent<Damageable>();
	}

	public GameObject GetTurretMount() => turretMount;
	public CharacterController GetCharacterController() => characterController;
	public Damageable GetDamageable() => damageable;
	public Transform GetTransform() => turretMount.transform;
}
