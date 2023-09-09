using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[SerializeField] private SpawnPoint playerSpawnPoint;

    [SerializeField] private List<SpawnPoint> enemiesSpawnPoints = new();

	private GameObject playerInstance; 

	private List<GameObject> enemyInstances = new();


	public void Init()
    {
		playerInstance = playerSpawnPoint.Spawn(true);
		for (int i = 0; i < enemiesSpawnPoints.Count; ++i)
		{
			enemyInstances.Add(enemiesSpawnPoints[i].Spawn(false));
		}
	}

	public GameObject GetPlayerInstance() => playerInstance;
	public List<GameObject> GetEnemyInstances() => enemyInstances;
}
