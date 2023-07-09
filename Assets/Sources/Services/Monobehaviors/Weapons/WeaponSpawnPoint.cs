using UnityEngine;

public class WeaponSpawnPoint : MonoBehaviour
{
	[SerializeField] private Transform weaponSpawPoint;
	public Transform WeaponPointSpawn => weaponSpawPoint;
}
