using UnityEngine;

public struct WeaponComponent
{
	public GameObject weaponInstance;
	public Weapon weapon;
	public TypeWeapon typeWeapon;
	public Transform pointSpawnWeapon;

	public string name;
	public int currentNumberWeapon;
	public int damage;
	public int speedAttack;
}
