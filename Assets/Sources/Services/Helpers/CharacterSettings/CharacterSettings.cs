using UnityEngine;

public class CharacterSettings : MonoBehaviour
{
	[Header("Weapon")]
	[SerializeField] private Transform pointSpawnWeapon;

	[Header("Motion")]
	[SerializeField] private float characterSpeed = 6.0f;
	[SerializeField] private float speedCloseCombatMove = 30.0f;
	[SerializeField] private bool isMovable = true;

	[Header("Dash")]
	[SerializeField] private float dashSpeed = 80.0f;
	[SerializeField] private float dashTime = 0.06f;

	[Header("Health")]
	[SerializeField] private int maxHealth = 100;

	[Header("Close combat")]
	[SerializeField] private int totalNumberStrikes = 4;

	public Transform GetPointSpawnWeapon() => pointSpawnWeapon;
	public float GetCharacterSpeed() => characterSpeed;
	public int GetMaxHealth() => maxHealth;
	public bool IsMovable() => isMovable;
	public int TotalNumberStrikes => totalNumberStrikes;
	public float SpeedCloseCombatMove => speedCloseCombatMove;
	public float DashSpeed => dashSpeed;
	public float DashTime => dashTime;

}
