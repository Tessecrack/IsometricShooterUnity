using System.Collections.Generic;
using UnityEngine;

public class SceneData : MonoBehaviour
{
	public List<Transform> EnemySpawnPoints = new List<Transform>();
	public Transform PlayerSpawnPoint;
	public Camera Camera { get; private set; }
	private void Start()
	{
		Camera = Camera.main;
	}
}
