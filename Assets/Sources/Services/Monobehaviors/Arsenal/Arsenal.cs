using System.Collections.Generic;
using UnityEngine;

public class Arsenal : MonoBehaviour
{
	[SerializeField]
	private List<GameObject> weapons;

	private WeaponsPool weaponsPool = new WeaponsPool();

	private readonly int maxCountWeapons = 3;

	public void Init(in Transform pointSpawnWeapon)
	{
		weaponsPool.InitWeapons(weapons, pointSpawnWeapon);
	}
	public Weapon GetWeapon(int numberSelectedWeapon)
	{
		return weaponsPool.Enable(numberSelectedWeapon);
	}
	public void HideWeapon(int numberSelectedWeapon)
	{
		weaponsPool.Disable(numberSelectedWeapon);
	}
	public MeleeWeapon GetMeleeWeapon()
	{
		return weaponsPool.CurrentMeleeWeapon;
	}

}
