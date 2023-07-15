using System.Collections.Generic;
using UnityEngine;

public class SceneData : MonoBehaviour
{
	public Camera Camera { get; private set; }
	public Spawner Spawner { get; private set; }

	public void Init()
	{
		Camera = Camera.main;
		Spawner = GetComponent<Spawner>();
		Spawner.Init();
	}

	public GameObject PlayerInstance => Spawner.GetPlayerInstance();
	public List<GameObject> EnemmiesInstances => Spawner.GetEnemyInstances();
	public List<GameObject> TurretsInstances => Spawner.GetTurretsInstances();

	public bool IsReadyScene => Spawner.IsSpawnedObjects;
}
