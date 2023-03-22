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
    protected abstract void ApplyMoveActor();
    protected abstract void ApplyRotationActor();
    protected abstract void ChangeWeapon();
    protected abstract void ApplyFire();

    private void ApplyAnimation() // need to change
    {
        animator.SetFloat("Horizontal", horizontalMovementValue);
        animator.SetFloat("Vertical", verticalMovementValue);
        animator.SetBool("IsRun", horizontalMovementValue != 0.0f || verticalMovementValue != 0.0f);
	}
}
