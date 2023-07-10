using System.Collections.Generic;
using UnityEngine;

public class WeaponsPool
{
	private static List<Weapon> weaponPool = new List<Weapon>();

	private Weapon currentMeleeWeapon;

	public void InitWeapons(List<GameObject> weaponPrefabs, Transform pointSpawnWeapon)
	{
		foreach (var weapon in weaponPrefabs)
		{
			var instance = UnityEngine.Object.Instantiate(weapon, pointSpawnWeapon, false);
			var weaponComponent = instance.GetComponent<Weapon>();
			if (weaponComponent.TypeWeapon == TypeWeapon.MELEE)
			{
				currentMeleeWeapon = weaponComponent;
			}
			instance.SetActive(false);
			weaponComponent.enabled = false;
			weaponPool.Add(weaponComponent);
		}
	}

	public Weapon Enable(int numberSelectedWeapon)
	{
		if (numberSelectedWeapon < 0 || numberSelectedWeapon >= weaponPool.Count)
		{
			return weaponPool[weaponPool.Count - 1];
		}
		weaponPool[numberSelectedWeapon].gameObject.SetActive(true);
		weaponPool[numberSelectedWeapon].enabled = true;
		return weaponPool[numberSelectedWeapon];
	}

	public void Disable(int numberSelectedWeapon)
	{
		if (numberSelectedWeapon < 0 || numberSelectedWeapon >= weaponPool.Count)
		{
			return;
		}
		weaponPool[numberSelectedWeapon].gameObject.SetActive(false);
		weaponPool[numberSelectedWeapon].enabled = false;
	}

	public Weapon CurrentMeleeWeapon => currentMeleeWeapon;
}
