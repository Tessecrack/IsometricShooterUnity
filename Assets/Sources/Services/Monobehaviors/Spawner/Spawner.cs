using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[SerializeField] private SpawnPoint playerSpawnPoint;

	[SerializeField] private List<SpawnPoint> turretsSpawnPoints = new();

    [SerializeField] private List<SpawnPoint> enemiesSpawnPoints = new();

	private GameObject playerInstance; 

	private List<GameObject> enemyInstances = new();

	private List<GameObject> turretInstances = new();

	public void Init()
    {
		playerInstance = playerSpawnPoint.Spawn(true);
		for (int i = 0; i < enemiesSpawnPoints.Count; ++i)
		{
			enemyInstances.Add(enemiesSpawnPoints[i].Spawn(false));
		}
		for (int i = 0; i < turretsSpawnPoints.Count; ++i)
		{
			turretInstances.Add(turretsSpawnPoints[i].Spawn(false));
		}
	}

	public GameObject GetPlayerInstance() => playerInstance;
	public List<GameObject> GetTurretsInstances() => turretInstances;
	public List<GameObject> GetEnemyInstances() => enemyInstances;
}
