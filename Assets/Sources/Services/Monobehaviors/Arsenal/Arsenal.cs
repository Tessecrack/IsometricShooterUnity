using System.Collections.Generic;
using UnityEngine;

public class Arsenal : MonoBehaviour
{
	[SerializeField] private List<Weapon> weapons;
	private WeaponsPool weaponsPool = new WeaponsPool();
	public void InitArsenal(List<GameObject> weaponPrefabs, Transform pointSpawnWeapon)
	{
		if (weapons == null || weapons.Count == 0)
		{
			weaponsPool.InitWeapons(weaponPrefabs, pointSpawnWeapon);
		}
	}
	public Weapon GetWeapon(int numberSelectedWeapon)
	{
		if (weapons.Count != 0)
		{
			
		}
		return weaponsPool.Enable(numberSelectedWeapon);
	}
	public void HideWeapon(int numberSelectedWeapon)
	{
		weaponsPool.Disable(numberSelectedWeapon);
	}
}
