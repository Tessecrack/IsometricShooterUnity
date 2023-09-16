using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
	[SerializeField] private GameObject spawnableObject;

	[SerializeField] private bool isEnabledPoint = true;

	public GameObject Spawn(bool needEnable)
	{
		var instance = Factory.CreateObject(spawnableObject, this.transform.position, Quaternion.identity);
		instance.SetActive(needEnable);
		return instance;
	}	

	public bool IsEnabledPoint => isEnabledPoint;
}
