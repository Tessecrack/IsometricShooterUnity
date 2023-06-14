using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StaticEnemies : ScriptableObject
{
	[Header("Range Enemy")]
	public List<GameObject> PrefabsRangeEnemy;

	[Header("Melee Enemy")]
	public List<GameObject> PrefabsMeleeEnemy;
}
