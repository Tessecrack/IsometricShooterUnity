using UnityEngine;

public class CharacterSettings : MonoBehaviour
{
	[Header("Main settings")]
	[SerializeField] private Transform pointSpawnWeapon;
	[SerializeField] private float characterSpeed = 6.0f;
	[SerializeField] private int maxHealth = 100;
	[SerializeField] private bool isMovable = true;
	public Transform GetPointSpawnWeapon() => pointSpawnWeapon;
	public float GetCharacterSpeed() => characterSpeed;
	public int GetMaxHealth() => maxHealth;
	public bool IsMovable() => isMovable;
}