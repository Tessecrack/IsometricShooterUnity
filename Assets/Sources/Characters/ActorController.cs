using System;
using System.Collections;
using UnityEngine;

public abstract class ActorController : MonoBehaviour
{
	public TypeWeapon CurrentTypeWeapon { get; protected set; }

	protected ActorMovement actorMovement;

	protected AttackMode attackMode;

	protected Arsenal arsenal;

	protected ActorHealth health;

	protected CharacterController characterController;

	private ActorAnimator actorAnimator;

	protected readonly int playerLayerMask = 7;
	protected abstract void UpdateMovementActor();
	protected abstract void UpdateTargetPoint();
	protected abstract void UpdateAttackMode();
	protected abstract void UpdateWeapon();
	protected abstract void SetDefaultWeapon();

	public Action<int> OnTakeDamage;
	public Action OnStartAttack;
	public Action OnStopAttack;
	public Action OnDash;
	
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
	}

	protected virtual void InitController()
	{
		attackMode = new AttackMode();
		actorMovement = new ActorMovement();
		health = new ActorHealth();

		characterController = GetComponent<CharacterController>();
		actorAnimator = new ActorAnimator(this, GetComponent<Animator>());
		arsenal = GetComponent<Arsenal>();

		OnStartAttack += StartAttack;
		OnStopAttack += StopAttack;
		OnTakeDamage += health.TakeDamage;
		OnDash += Dash;	
		SetDefaultWeapon();
	}

	protected virtual void UpdateAnimation()
	{
		actorAnimator.Animate();
	}

	protected virtual void StartAttack()
	{
		attackMode.Enable();
		var currentWeapon = this.GetCurrentWeapon();
		currentWeapon.StartAttack(this, actorMovement.GetTargetPoint());
	}

	protected virtual void StopAttack()
	{
		attackMode.Disable();
		this.GetCurrentWeapon().StopAttack();
	}
	protected virtual void ApplyMovementActor()
	{
		SetDirectionMovement();
		MoveActor(actorMovement.Speed);
	}
	protected virtual void ApplyRotationActor()
	{
		var direction = attackMode.IsActive ? actorMovement.GetTargetPoint() - this.transform.position : actorMovement.ActorVelocityVector;
		this.transform.forward = actorMovement.Rotate(this.transform.forward, 
			direction, Time.fixedDeltaTime);
	}

	public bool IsActiveAttackMode()
	{
		return attackMode.IsActive;
	}

	public Weapon GetCurrentWeapon()
	{
		return arsenal.GetCurrentWeapon();
	}

	public void TakeDamage(int damage)
	{
		OnTakeDamage?.Invoke(damage);
		if (health.IsDead)
		{
			Destroy(this.gameObject);
		}
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
	
	private void Dash()
	{
		StartCoroutine(ApplyDash());
	}
	
	private IEnumerator ApplyDash()
	{
		float passedTime = 0;
		while(passedTime <= 0.05f)
		{
			passedTime += Time.fixedDeltaTime;
			MoveActor(actorMovement.SpeedDash);
			yield return new WaitForFixedUpdate();
		}
	}
	
	public Vector3 GetForwardVector() => this.transform.forward;
	public float GetForwardMovementValue() => actorMovement.ForwardMovementValue;
	public float GetRightMovementValue() => actorMovement.RightMovementValue;
	public float GetDirectionForwardMovementValue() => actorMovement.DirectionForwardMotion;
	public float GetDirectionRightMovementValue() => actorMovement.DirectionRightMotion;
}
