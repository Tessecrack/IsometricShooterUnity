using Assets.Sources.Characters;
using UnityEngine;

public abstract class ActorController : MonoBehaviour
{
	public float ForwardMovementValue { get; protected set; } // w s
	public float RightMovementValue { get; protected set; } // a d
	public float DirectionForwardMotion { get; protected set; }
	public float DirectionRightMotion { get; protected set; }
	public TypeWeapon CurrentTypeWeapon { get; protected set; }


	protected int speed;

	protected AttackMode attackMode;

	protected Arsenal arsenal;

	protected Vector3 initialActorForwardVector = new Vector3(-1.0f, 0.0f, 1.0f);
	protected Vector3 initialActorRightVector = new Vector3(1.0f, 0.0f, 1.0f);

	protected CharacterController characterController;

	protected Vector3 targetPoint = Vector3.zero;
	protected Vector3 actorVelocityVector;

	private ActorAnimator actorAnimator;

	private void Start()
	{
		InitController();
	}

	private void Update()
	{
		ApplyTargetPoint();
		ApplyAttack();
		ChangeWeapon();
		ApplyAnimation();
	}

	private void FixedUpdate()
	{
		ApplyMoveActor();
		ApplyRotationActor();
	}

	protected virtual void InitController()
	{
		attackMode = new AttackMode(this);
		characterController = GetComponent<CharacterController>();
		arsenal = GetComponent<Arsenal>();
		actorAnimator = new ActorAnimator(this, GetComponent<Animator>());
	}
	protected virtual void ApplyMoveActor()
	{
		SetDirectionMovement();
		actorVelocityVector = GetVelocity();
		var vectorMove = new Vector3(actorVelocityVector.x * speed, 0.0f, actorVelocityVector.z * speed);
		characterController.Move(vectorMove * Time.fixedDeltaTime);
	}
	protected virtual void ApplyRotationActor()
	{
		var direction = attackMode.IsActiveAttackMode ? targetPoint - this.transform.position : actorVelocityVector;
		this.transform.forward = Vector3.RotateTowards(this.transform.forward,
			direction,
			Time.fixedDeltaTime * 20, 0.0f);
	}
	protected abstract void ApplyTargetPoint();
	protected abstract void ChangeWeapon();
	protected abstract void ApplyAttack();
	private Vector3 GetVelocity()
	{
		var currentVelocity = ForwardMovementValue * initialActorForwardVector + RightMovementValue * initialActorRightVector;

		var normalizeVelocity = new Vector3(
			Mathf.Clamp(currentVelocity.x, -1.5f, 1.5f),
			currentVelocity.y,
			Mathf.Clamp(currentVelocity.z, -1.5f, 1.5f));

		return normalizeVelocity;
	}
	private void ApplyAnimation()
	{
		actorAnimator.Animate();
	}

	private void SetDirectionMovement()
	{
		var movementVector = Vector3.ClampMagnitude(actorVelocityVector, 1);
		var relativeVector = this.transform.InverseTransformDirection(movementVector);

		DirectionRightMotion = relativeVector.x;
		DirectionForwardMotion = relativeVector.z;
	}

	public bool IsActiveAttackMode()
	{
		return attackMode.IsActiveAttackMode;
	}

	public Weapon GetCurrentWeapon()
	{
		return arsenal.GetCurrentWeapon();
	}

	public void DeactivateAttackMode()
	{
		attackMode.DeactivateAttackMode();
	}
}
