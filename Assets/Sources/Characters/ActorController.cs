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

    private void ApplyAnimation()
    {
        bool isIdle = horizontalMovementValue == 0 && verticalMovementValue == 0;
        var currentTypeWeapon = arsenal?.GetCurrentWeapon()?.typeWeapon;        
        if (isIdle)
        {
            if (currentTypeWeapon == null)
            {
                animator.Play("IdleGun");
                return;
            }
            switch(currentTypeWeapon)
            {
                case TypeWeapon.GUN: 
                    animator.Play("IdleGun"); 
                    break;
                case TypeWeapon.HEAVY: 
                    animator.Play("Idle"); 
                    break;
            }
        }
        else
        {
            animator.Play("Run");
        }
    }
}
