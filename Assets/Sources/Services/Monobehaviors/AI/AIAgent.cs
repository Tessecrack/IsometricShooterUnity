using UnityEngine;

public class AIAgent : MonoBehaviour
{
	[SerializeField] private float distanceDetection = 5.0f;
	[SerializeField] private float rangeAttack = 1.5f;

	private bool isTargetDetected;
	private bool canAttack;
	public bool IsDetectTarget(Vector3 target)
	{
		var distance = target - transform.position;
		return isTargetDetected = distanceDetection >= distance.magnitude;
	}

	public bool CanAttack(Vector3 target)
	{
		var distance = target - transform.position;
		return canAttack = rangeAttack >= target.magnitude;
	}
}
