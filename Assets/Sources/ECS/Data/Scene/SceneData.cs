using System.Collections.Generic;
using UnityEngine;

public class SceneData : MonoBehaviour
{
	[Header("Player")]
	public Transform PlayerSpawnPoint;

	[Header("Enemies melee")]
	public List<Transform> EnemyMeleeSpawnPoints = new List<Transform>();

	[Header("Enemies range")]
	public List<Transform> EnemyRangeSpawnPoints = new List<Transform>();

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
