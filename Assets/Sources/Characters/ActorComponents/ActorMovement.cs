using UnityEngine;

public class ActorMovement
{ 
	public float ForwardMovementValue { get; protected set; } // w s
	public float RightMovementValue { get; protected set; } // a d
	public float LocalDirectionForwardMotion { get; protected set; }
	public float LocalDirectionRightMotion { get; protected set; }

	public int Speed { get; protected set; }

	public int SpeedDash { get; protected set; }

	public Vector3 ActorVelocityVector { get; protected set; }

	private readonly float speedRotation = 20.0f;

	protected Vector3 initialActorForwardVector = new Vector3(-1.0f, 0.0f, 1.0f);
	protected Vector3 initialActorRightVector = new Vector3(1.0f, 0.0f, 1.0f);
	protected Vector3 targetPoint = Vector3.zero;

	public void InitInitialOptions(int speed, Vector3 initialForwardVector, Vector3 initialRightVector)
	{
		initialActorForwardVector = initialForwardVector;
		initialActorRightVector = initialRightVector;
		Speed = speed;
		SpeedDash = 20;
	}

	public Vector3 Rotate(Vector3 forwardVector, Vector3 direction, float time) 
		=> Vector3.RotateTowards(forwardVector, direction, time * speedRotation, 0.0f);

	private Vector3 GetVelocity()
	{
		var currentVelocity = 
			Vector3.ClampMagnitude(ForwardMovementValue * initialActorForwardVector + RightMovementValue * initialActorRightVector, 1.5f);

		return currentVelocity;
	}

	public void SetLocalDirectionMovement(Vector3 relativeVector)
	{
		LocalDirectionRightMotion = relativeVector.x;
		LocalDirectionForwardMotion = relativeVector.z;
	}

	public Vector3 GetSpeedVelocity(float speed)
	{
		ActorVelocityVector = GetVelocity();
		return new Vector3(ActorVelocityVector.x * speed, 0.0f, ActorVelocityVector.z * speed);
	}

	public void UpdateTargetPoint(Vector3 newTargetPoint)
	{
		targetPoint = newTargetPoint;
	}
	public Vector3 GetTargetPoint() => targetPoint;

	public void SetForwardMovementValue(float value)
	{
		ForwardMovementValue = value;
	}
	public void SetRightMovementValue(float value)
	{
		RightMovementValue = value;
	}
}
