using UnityEngine;

public class AIAgent : MonoBehaviour
{
	private TypeAttack typeAttack;

	private float rangeDetection = 5.0f;
	private float rangeMeleeAttack = 1.5f;
	private float distanceRangeAttack = 5.0f;

	private bool isTargetDetected;
	private bool canAttack;

	public void SetTypeAttack(TypeAttack typeAttack)
	{
		this.typeAttack = typeAttack;
	}

	public void SetRangeDetection(float rangeDetection)
	{
		this.rangeDetection = rangeDetection;
	}

	public void SetRangeMeleeAttack(float rangeMeleeAttack)
	{
		this.rangeMeleeAttack = rangeMeleeAttack;
	}

	public void SetDistanceRangeAttack(float distanceRangeAttack)
	{
		this.distanceRangeAttack = distanceRangeAttack;
	}

	public bool IsDetectTarget(Vector3 target)
	{
		var distance = target - transform.position;
		return isTargetDetected = rangeDetection >= distance.magnitude;
	}

	public bool CanMeleeAttack(Vector3 target)
	{
		if (typeAttack == TypeAttack.Melee)
		{
			var distance = target - transform.position;
			return canAttack = rangeMeleeAttack >= distance.magnitude;
		}
		return false;
	}

	public bool CanRangeAttack(Vector3 target)
	{
		if (typeAttack == TypeAttack.Range)
		{
			var distance = target - transform.position;
			return canAttack = distanceRangeAttack >= distance.magnitude;
		}
		return false;
	}

	public float RangeAttack => rangeMeleeAttack;
}
