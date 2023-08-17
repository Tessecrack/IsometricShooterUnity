using UnityEngine;

public class Factory
{
	public static T CreateObject<T>(T prefab, Vector3 position, Quaternion rotation) where T : MonoBehaviour
	{
		return Object.Instantiate<T>(prefab, position, rotation);
	}

	public static GameObject CreateObject(GameObject prefab, Vector3 position, Quaternion rotation)
	{
		return Object.Instantiate(prefab, position, rotation);
	}

	public static GameObject CreateObject(GameObject prefab, Transform transform, bool worldPosition)
	{
		return Object.Instantiate(prefab, transform, worldPosition);
	}
}
