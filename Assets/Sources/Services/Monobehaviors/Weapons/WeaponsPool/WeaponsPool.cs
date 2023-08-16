using System.Collections.Generic;
using UnityEngine;

public class WeaponsPool
{
	private List<Weapon> currentWeaponsPool = new();

	private Weapon currentMeleeWeapon;

	private Weapon currentGunWeapon;

	private Weapon currentHeavyWeapon;

	public void InitWeapons(in List<GameObject> weaponPrefabs, in Transform pointSpawnWeapon)
	{
		foreach (var weapon in weaponPrefabs)
		{
			var instance = Factory.CreateObject(weapon, pointSpawnWeapon, false);
			var weaponComponent = instance.GetComponent<Weapon>();
			weaponComponent.SetGripWeapon();
			instance.SetActive(false);
			weaponComponent.enabled = false;
			currentWeaponsPool.Add(weaponComponent);
			if (weaponComponent.TypeWeapon == TypeWeapon.MELEE)
			{
				currentMeleeWeapon = weaponComponent;
			}
			else if (weaponComponent.TypeWeapon == TypeWeapon.GUN)
			{
				currentGunWeapon = weaponComponent;
			}
			else if (weaponComponent.TypeWeapon == TypeWeapon.HEAVY)
			{
				currentHeavyWeapon = weaponComponent;
			}
		}
	}

	public Weapon Enable(int numberSelectedWeapon)
	{
		if (numberSelectedWeapon < 0 || numberSelectedWeapon >= currentWeaponsPool.Count)
		{
			return currentWeaponsPool[currentWeaponsPool.Count - 1];
		}
		currentWeaponsPool[numberSelectedWeapon].gameObject.SetActive(true);
		currentWeaponsPool[numberSelectedWeapon].enabled = true;
		return currentWeaponsPool[numberSelectedWeapon];
	}

	public void Disable(int numberSelectedWeapon)
	{
		if (numberSelectedWeapon < 0 || numberSelectedWeapon >= currentWeaponsPool.Count)
		{
			return;
		}
		currentWeaponsPool[numberSelectedWeapon].gameObject.SetActive(false);
		currentWeaponsPool[numberSelectedWeapon].enabled = false;
	}

	public Weapon CurrentMeleeWeapon => currentMeleeWeapon;
	public Weapon CurrentGunWeapon => currentGunWeapon;
	public Weapon CurrentHeavyWeapon => currentHeavyWeapon;
}
