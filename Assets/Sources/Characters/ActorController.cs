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
    }
    protected virtual void ApplyMoveActor()
    {
		actorVelocityVector = GetVelocity();
		var vectorMove = new Vector3(actorVelocityVector.x * speed, 0.0f, actorVelocityVector.z * speed);
		characterController.Move(vectorMove * Time.fixedDeltaTime);
    }
    protected virtual void ApplyRotationActor()
    {
		this.transform.forward = Vector3.RotateTowards(this.transform.forward, 
            new Vector3(-verticalMovementValue, 0.0f, horizontalMovementValue), 
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
        return verticalMovementValue * initialActorForwardVector + horizontalMovementValue * initialActorRightVector;
	}
	private void ApplyAnimation() // need to change
    	{
        	animator.SetFloat("Horizontal", horizontalMovementValue);
        	animator.SetFloat("Vertical", verticalMovementValue);
        	animator.SetBool("IsRun", horizontalMovementValue != 0.0f || verticalMovementValue != 0.0f);
	}
}
