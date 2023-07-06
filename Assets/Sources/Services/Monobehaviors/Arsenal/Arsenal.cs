using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arsenal : MonoBehaviour
{
	[SerializeField] private List<Weapon> weapons;
	private WeaponsPool weaponsPool = new WeaponsPool();
	public void InitArsenal(List<GameObject> weaponPrefabs, Transform pointSpawnWeapon)
	{
		weaponsPool.InitWeapons(weaponPrefabs, pointSpawnWeapon);
	}
	public (GameObject instance, Weapon weapon) GetWeapon(int numberSelectedWeapon) => weaponsPool.Enable(numberSelectedWeapon);
	public void HideWeapon(int numberSelectedWeapon) => weaponsPool.Disable(numberSelectedWeapon);

	public Weapon CurrentMeleeWeapon => weaponsPool.CurrentMeleeWeapon;
}
