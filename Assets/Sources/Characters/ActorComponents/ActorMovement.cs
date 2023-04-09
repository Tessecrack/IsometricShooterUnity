using UnityEngine;

public class ActorMovement
{ 
	public float ForwardMovementValue { get; protected set; } // w s
	public float RightMovementValue { get; protected set; } // a d
	public float DirectionForwardMotion { get; protected set; }
	public float DirectionRightMotion { get; protected set; }

	public int Speed { get; protected set; }

	public int SpeedDash { get; protected set; }

	public bool IsDash { get; set; } = false;

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

	public Vector3 GetVelocity()
	{
		var currentVelocity = ForwardMovementValue * initialActorForwardVector + RightMovementValue * initialActorRightVector;

		var normalizeVelocity = new Vector3(
			Mathf.Clamp(currentVelocity.x, -1.5f, 1.5f),
			currentVelocity.y,
			Mathf.Clamp(currentVelocity.z, -1.5f, 1.5f));

		return normalizeVelocity;
	}

	public void SetDirectionMovement(Vector3 relativeVector)
	{
		DirectionRightMotion = relativeVector.x;
		DirectionForwardMotion = relativeVector.z;
	}

	public Vector3 GetMoveActor(float speed)
	{
		ActorVelocityVector = GetVelocity();
		var vectorMove = new Vector3(ActorVelocityVector.x * speed, 0.0f, ActorVelocityVector.z * speed);
		return vectorMove;
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
