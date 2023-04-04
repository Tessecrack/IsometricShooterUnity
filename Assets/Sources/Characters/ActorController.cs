using Assets.Sources.Characters;
using System.Collections;
using UnityEngine;

public abstract class ActorController : MonoBehaviour
{
	public TypeWeapon CurrentTypeWeapon { get; protected set; }

	protected ActorMovement actorMovement;

	protected AttackMode attackMode;

	protected Arsenal arsenal;

	protected CharacterController characterController;

	private ActorAnimator actorAnimator;

	protected int currentWeaponNumber = 1;

	protected abstract void UpdateMovementActor();
	protected abstract void UpdateTargetPoint();
	protected abstract void UpdateAttackMode();
	protected abstract void UpdateWeapon();

	private void Start()
	{
		InitController();
	}

	private void Update()
	{
		UpdateMovementActor();
		UpdateTargetPoint();
		UpdateWeapon();
		UpdateAttackMode();
		UpdateAnimation();
	}

	private void FixedUpdate()
	{
		ApplyMovementActor();
		ApplyRotationActor();
		ApplyWeapon();
		ApplyAttack();
	}

	protected virtual void InitController()
	{
		attackMode = new AttackMode(this);
		actorAnimator = new ActorAnimator(this, GetComponent<Animator>());
		actorMovement = new ActorMovement();
		characterController = GetComponent<CharacterController>();
		arsenal = GetComponent<Arsenal>();
		
	}
	private void UpdateAnimation()
	{
		actorAnimator.Animate();
	}

	protected virtual void ApplyAttack()
	{
		if (attackMode.IsStartAttack)
		{
			attackMode.StartAttack(actorMovement.GetTargetPoint());
		}
		if (attackMode.IsStopAttack)
		{
			attackMode.StopAttack();
		}
		attackMode.Reset();
		attackMode.IncreaseCurrentTimeAttackMode(Time.fixedDeltaTime);
	}

	protected virtual void ApplyMovementActor()
	{
		SetDirectionMovement();
		if (actorMovement.IsDash)
		{
			StartCoroutine(ApplyDash());
		}
		else
		{
			MoveActor(actorMovement.Speed);
		}
	}
	protected virtual void ApplyRotationActor()
	{
		var direction = attackMode.IsActiveAttackMode ? actorMovement.GetTargetPoint() - this.transform.position : actorMovement.ActorVelocityVector;
		this.transform.forward = actorMovement.Rotate(this.transform.forward, 
			direction, Time.fixedDeltaTime);
	}

	protected virtual void ApplyWeapon()
	{
		arsenal.ChangeWeapon(currentWeaponNumber);
		CurrentTypeWeapon = arsenal.GetCurrentWeapon().CurrentTypeWeapon;
	}
	public bool IsActiveAttackMode()
	{
		return attackMode.IsActiveAttackMode;
	}

	public Weapon GetCurrentWeapon()
	{
		return arsenal.GetCurrentWeapon();
	}

	private void SetDirectionMovement()
	{
		var movementVector = Vector3.ClampMagnitude(actorMovement.ActorVelocityVector, 1);
		var relativeVector = this.transform.InverseTransformDirection(movementVector);
		actorMovement.SetDirectionMovement(relativeVector);
	}

	private void MoveActor(float speed)
	{
		characterController.Move(actorMovement.GetMoveActor(speed) * Time.fixedDeltaTime);
	}

	private IEnumerator ApplyDash()
	{
		float passedTime = 0;
		while(passedTime <= 0.05f)
		{
			passedTime += Time.fixedDeltaTime;
			MoveActor(20.0f);
			yield return new WaitForFixedUpdate();
		}
		actorMovement.IsDash = false;
	}
	public Vector3 GetForwardVector() => this.transform.forward;
	public float GetForwardMovementValue() => actorMovement.ForwardMovementValue;
	public float GetRightMovementValue() => actorMovement.RightMovementValue;
	public float GetDirectionForwardMovementValue() => actorMovement.DirectionForwardMotion;
	public float GetDirectionRightMovementValue() => actorMovement.DirectionRightMotion;
}
