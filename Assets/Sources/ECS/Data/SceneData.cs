using System.Collections.Generic;
using UnityEngine;

public class SceneData : MonoBehaviour
{
	public List<Vector3> enemySpawnPoints = new List<Vector3>();
	public Transform playerSpawnPoint;
	public Camera camera;
	public void SetCamera()
	{
		if (camera == null)
		{
			camera = Camera.main;
		}
	}
}
