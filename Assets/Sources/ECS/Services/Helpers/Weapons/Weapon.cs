using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	public string NameWeapon;

	public TypeWeapon TypeWeapon;

	public bool IsAutomaticWeapon;

	public GameObject prefabProjectTile;

	public List<GameObject> muzzles;

	public int quantityOneShotBullet = 1;

	public void StartAttack(Transform startTrasform, Vector3 targetPosition)
	{
		Debug.Log("START");
	}

	public void StopAttack()
	{
		Debug.Log("STOP");
	}
}
