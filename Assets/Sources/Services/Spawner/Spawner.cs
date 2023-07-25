using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[SerializeField] private SpawnPoint playerSpawnPoint;

	[SerializeField] private List<SpawnPoint> turretsSpawnPoints = new List<SpawnPoint>();

    [SerializeField] private List<SpawnPoint> enemiesSpawnPoints = new List<SpawnPoint>();

	private GameObject playerInstance; 

	private List<GameObject> enemyInstances = new List<GameObject>();

	private List<GameObject> turretInstances = new List<GameObject>();

	private bool isTurretsSpawned = false;
	private bool isEnemiesSpawned = false;

	public void Init()
    {
		playerInstance = playerSpawnPoint.Spawn(true);
		for (int i = 0; i < enemiesSpawnPoints.Count; ++i)
		{
			if (enemiesSpawnPoints[i] == null)
			{
				continue;
			}

			enemyInstances.Add(enemiesSpawnPoints[i].Spawn(false));
		}
		for (int i = 0; i < turretsSpawnPoints.Count; ++i)
		{
			turretInstances.Add(turretsSpawnPoints[i].Spawn(false));
		}
		//StartCoroutine(SpawnEnemies());
		//StartCoroutine(SpawnTurrets());
	}

	IEnumerator SpawnEnemies()
	{
		for (int i = 0; i < enemiesSpawnPoints.Count; ++i)
		{
			enemyInstances.Add(enemiesSpawnPoints[i].Spawn(false));
			yield return null;
		}
		isEnemiesSpawned = true;
	}

	IEnumerator SpawnTurrets()
	{
		for (int i = 0; i < turretsSpawnPoints.Count; ++i)
		{
			turretInstances.Add(turretsSpawnPoints[i].Spawn(false));
			yield return null;
		}
		isTurretsSpawned = true;
	}

	public GameObject GetPlayerInstance() => playerInstance;
	public List<GameObject> GetTurretsInstances() => turretInstances;
	public List<GameObject> GetEnemyInstances() => enemyInstances;
	public bool IsSpawnedObjects => isTurretsSpawned && isEnemiesSpawned;
}
