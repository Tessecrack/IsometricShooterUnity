using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
	[SerializeField] private GameObject spawnableObject;
	public GameObject Spawn(bool needEnable)
	{
		var instance = UnityEngine.Object.Instantiate(spawnableObject, this.transform.position, Quaternion.identity);
		instance.SetActive(needEnable);
		return instance;
	}
}
