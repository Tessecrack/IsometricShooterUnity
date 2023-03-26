using UnityEngine;

public abstract class ActorController : MonoBehaviour
{
	private Animator animator;

	protected int speed;
	protected float horizontalMovementValue; // a d
	protected float verticalMovementValue; // w s
	protected readonly float timeAttackMode = 3.0f;
	protected float currentTimeAttackMode = 0.0f;
	protected bool isAttackMode = false;

	protected Arsenal arsenal;

	protected readonly Vector3 initialActorForwardVector = new Vector3(-1.0f, 0.0f, 1.0f);
	protected readonly Vector3 initialActorRightVector = new Vector3(1.0f, 0.0f, 1.0f);

	protected CharacterController characterController;

	protected Vector3 targetPoint = Vector3.zero;
	protected Vector3 actorVelocityVector;
	protected TypeWeapon currentTypeWeapon;
	private void Start()
	{
		InitController();
	}

	private void Update()
	{
		ApplyTargetPoint();
		ApplyAttack();
		ApplyAttackMode();
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
		characterController = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
		arsenal = GetComponent<Arsenal>();
	}
	protected virtual void ApplyMoveActor()
	{
		actorVelocityVector = GetVelocity();
		var vectorMove = new Vector3(actorVelocityVector.x * speed, 0.0f, actorVelocityVector.z * speed);
		characterController.Move(vectorMove * Time.fixedDeltaTime);
	}
	protected virtual void ApplyRotationActor()
	{
		var direction = isAttackMode ? targetPoint - this.transform.position : actorVelocityVector;
		this.transform.forward = Vector3.RotateTowards(this.transform.forward,
			direction,
			Time.fixedDeltaTime * 20, 0.0f);
	}
	protected virtual void ApplyAttackMode()
	{
		if (isAttackMode)
		{
			currentTimeAttackMode += Time.deltaTime;
		}
		if (currentTimeAttackMode >= timeAttackMode)
		{
			isAttackMode = false;
			currentTimeAttackMode = 0;
		}
	}
	protected abstract void ApplyTargetPoint();
	protected abstract void ChangeWeapon();
	protected abstract void ApplyAttack();
	private Vector3 GetVelocity()
	{
		var currentVelocity = verticalMovementValue * initialActorForwardVector + horizontalMovementValue * initialActorRightVector;

		var normalizeVelocity = new Vector3(
			Mathf.Clamp(currentVelocity.x, -1.5f, 1.5f),
			currentVelocity.y,
			Mathf.Clamp(currentVelocity.z, -1.5f, 1.5f));

		return normalizeVelocity;
	}
	private void ApplyAnimation() // need to change
	{
		animator.SetFloat(AnimationParams.FLOAT_HORIZONTAL_MOTION_NAME_PARAM, horizontalMovementValue);
		animator.SetFloat(AnimationParams.FLOAT_VERTICAL_MOTION_NAME_PARAM, verticalMovementValue);
		animator.SetBool(AnimationParams.BOOL_RUN_NAME_PARAM, horizontalMovementValue != 0.0f || verticalMovementValue != 0.0f);
		animator.SetBool(AnimationParams.BOOL_ATTACK_MODE_NAME_PARAM, isAttackMode);
		animator.SetBool(AnimationParams.BOOL_MELEE_WEAPON_NAME_PARAM, currentTypeWeapon == TypeWeapon.MELEE);
		animator.SetBool(AnimationParams.BOOL_GUN_WEAPON_NAME_PARAM, currentTypeWeapon == TypeWeapon.GUN);
		animator.SetBool(AnimationParams.BOOL_HEAVY_WEAPON_NAME_PARAM, currentTypeWeapon == TypeWeapon.HEAVY);
	}
}
