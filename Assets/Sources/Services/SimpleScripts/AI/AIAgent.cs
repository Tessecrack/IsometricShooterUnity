using UnityEngine;

public class AIAgent
{
	private Transform transform;
	
	private bool hasArsenal;
	private bool hasRangeAttack;
	private bool hasMeleeAttack;

	private float rangeDetection = 5.0f;
	private float rangeMeleeAttack = 1.5f;
	private float distanceRangeAttack = 5.0f;

	private bool isTargetDetected;
	private bool canAttack;

	public void SetHasArsenal(bool hasArsenal)
	{
		this.hasArsenal = hasArsenal;
	}

	public void SetRangeAttack(bool hasRangeAttack)
	{
		this.hasRangeAttack = hasRangeAttack;
	}

	public void SetMeleeAttack(bool hasMeleeAttack)
	{
		this.hasMeleeAttack = hasMeleeAttack;
	}

	public void SetTransform(Transform transform)
	{
		this.transform = transform;
	}

	public void SetRangeDetection(float rangeDetection)
	{
		this.rangeDetection = rangeDetection;
	}

	public void SetDistanceMeleeAttack(float rangeMeleeAttack)
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
		if (hasMeleeAttack)
		{
			var distance = target - transform.position;
			return canAttack = rangeMeleeAttack >= distance.magnitude;
		}
		return false;
	}

	public bool CanRangeAttack(Vector3 target)
	{
		if (hasRangeAttack)
		{
			var distance = target - transform.position;
			return canAttack = distanceRangeAttack >= distance.magnitude;
		}
		return false;
	}

	public float RangeAttack => rangeMeleeAttack;
}
