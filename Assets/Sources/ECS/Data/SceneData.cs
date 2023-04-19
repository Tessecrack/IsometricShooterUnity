using UnityEngine;

public class SceneData : MonoBehaviour
{
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
