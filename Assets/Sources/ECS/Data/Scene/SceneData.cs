using System.Collections.Generic;
using UnityEngine;

public class SceneData : MonoBehaviour
{
	[Header("Player")]
	public Transform PlayerSpawnPoint;

	[Header("Enemies")]
	public List<Transform> EnemySpawnPoints = new List<Transform>();

	[Header("Enemy turrets")]
	public List<Transform> EnemyTurretsSpawnPoints = new List<Transform>();

	[Header("Friendly turrets")]
	public List<Transform> FriendlyTurretsSpawnPoints = new List<Transform>();

	public Camera Camera { get; private set; }
	private void Start()
	{
		Camera = Camera.main;
	}
}
