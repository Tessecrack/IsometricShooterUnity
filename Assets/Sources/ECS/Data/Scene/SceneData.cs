using System.Collections.Generic;
using UnityEngine;

public class SceneData : MonoBehaviour
{
	[Header("Player")]
	public Transform PlayerSpawnPoint;

	[Header("Enemies")]
	public List<Transform> EnemySpawnPoints = new List<Transform>();

	[Header("Enemy blue turrets")]
	public GameObject EnemyBlueTurretsSpawnPoints;

	[Header("Enemy red turrets")]
	public GameObject EnemyRedTurretsSpawnPoints;

	[Header("Friendly turrets")]
	public GameObject FriendlyGreenTurretsSpawnPoints;
	public Camera Camera { get; private set; }

	public Transform[] BlueTurretsSpawnPoints { get; private set; }
	public Transform[] RedTurretsSpawnPoints { get; private set; }
	public Transform[] GreenTurretsSpawnPoints { get; private set; }

	private void Awake()
	{
		Camera = Camera.main;
		BlueTurretsSpawnPoints = EnemyBlueTurretsSpawnPoints.GetComponentsInChildren<Transform>();
		RedTurretsSpawnPoints = EnemyRedTurretsSpawnPoints.GetComponentsInChildren<Transform>();
		GreenTurretsSpawnPoints = FriendlyGreenTurretsSpawnPoints.GetComponentsInChildren<Transform>();
	}
}
