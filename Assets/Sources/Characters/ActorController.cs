using UnityEngine;

public abstract class ActorController : MonoBehaviour
{
    protected Arsenal arsenal;
    protected float horizontalMovementValue; // a d
    protected float verticalMovementValue; // w s

    protected int speed;

    protected readonly Vector3 initialActorForwardVector = new Vector3(-1.0f, 0.0f, 1.0f);
    protected readonly Vector3 initialActorRightVector = new Vector3(1.0f, 0.0f, 1.0f);

    protected CharacterController characterController;

    protected Vector3 actorVelocityVector;

    private Animator animator;

    private void Start()
    {
        InitController();
        arsenal = GetComponent<Arsenal>();
    }

    private void Update()
    {
        if (arsenal == null)
        {
            Debug.Log("NULL");
        }
        ApplyFire();
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
		actorVelocityVector = verticalMovementValue * initialActorForwardVector + horizontalMovementValue * initialActorRightVector;
		var vectorMove = new Vector3(actorVelocityVector.x * speed, 0.0f, actorVelocityVector.z * speed);
		characterController.Move(vectorMove * Time.fixedDeltaTime);
	}

    protected virtual void ApplyRotationActor()
    {
		var startPosition = this.transform.position;
		var endPosition = GetVelocity();

		var direction = Vector3.RotateTowards(this.transform.forward, endPosition - startPosition, Time.fixedDeltaTime * 20, 0.0f);
		this.transform.rotation = Quaternion.LookRotation(direction);
	}

	protected virtual Vector3 GetVelocity()
	{
        return verticalMovementValue * initialActorForwardVector + horizontalMovementValue * initialActorRightVector;
	}

	protected abstract void ChangeWeapon();
    protected abstract void ApplyFire();

    private void ApplyAnimation() // need to change
    {
        animator.SetFloat("Horizontal", horizontalMovementValue);
        animator.SetFloat("Vertical", verticalMovementValue);
        animator.SetBool("IsRun", horizontalMovementValue != 0.0f || verticalMovementValue != 0.0f);
	}
}
