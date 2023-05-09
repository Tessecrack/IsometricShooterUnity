using System.Collections.Generic;
using UnityEngine;

public class SceneData : MonoBehaviour
{
	public List<Vector3> enemySpawnPoints = new List<Vector3>();
	public Transform playerSpawnPoint;
	public Camera Camera { get; private set; }
	private void Start()
	{
		Camera = Camera.main;
	}
}
