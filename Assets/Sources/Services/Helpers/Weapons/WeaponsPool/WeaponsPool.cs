using System.Collections.Generic;
using UnityEngine;

public class WeaponsPool
{
	private static List<(GameObject instance, Weapon weaponComponent)> weaponPool
		= new List<(GameObject instance, Weapon weaponComponent)>();

	public void InitWeapons(List<GameObject> weaponPrefabs, Transform pointSpawnWeapon)
	{
		foreach (var weapon in weaponPrefabs)
		{
			var instance = UnityEngine.Object.Instantiate(weapon, pointSpawnWeapon, false);
			var weaponComponent = instance.GetComponent<Weapon>();
			instance.SetActive(false);
			weaponComponent.enabled = false;

			weaponPool.Add((instance, weaponComponent));
		}
	}

	public (GameObject instance, Weapon weapon) Enable(int numberSelectedWeapon)
	{
		if (numberSelectedWeapon < 0 || numberSelectedWeapon >= weaponPool.Count)
		{
			return weaponPool[weaponPool.Count - 1];
		}
		weaponPool[numberSelectedWeapon].instance.SetActive(true);
		weaponPool[numberSelectedWeapon].weaponComponent.enabled = true;
		return weaponPool[numberSelectedWeapon];
	}

	public void Disable(int numberSelectedWeapon)
	{
		if (numberSelectedWeapon < 0 || numberSelectedWeapon >= weaponPool.Count)
		{
			return;
		}
		weaponPool[numberSelectedWeapon].instance.SetActive(false);
		weaponPool[numberSelectedWeapon].weaponComponent.enabled = false;
	}
}
