using UnityEngine;

public class AIAgent : MonoBehaviour
{
	[SerializeField] private float distanceDetection = 5.0f;
	
	public virtual bool IsDetectTarget(Vector3 target)
	{
		var distance = target - transform.position;
		return distanceDetection >= distance.magnitude;
	}
}
