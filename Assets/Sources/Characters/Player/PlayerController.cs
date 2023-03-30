using UnityEngine;


public class PlayerController : ActorController
{
    [SerializeField] private PlayerCamera playerCamera;
    protected override void InitController()
    {
        base.InitController();
        if (playerCamera == null)
        {
			playerCamera = FindObjectOfType<PlayerCamera>();
		}
        if (playerCamera != null)
        {
            playerCamera.SetOwner(this.gameObject);
        }
        this.initialActorForwardVector = playerCamera.ForwardVector;
        this.initialActorRightVector = playerCamera.RightVector;
        speed = 6;
    }

    protected override void ApplyMoveActor()
    {
		rightMovementValue = Input.GetAxis("Horizontal");
        forwardMovementValue = Input.GetAxis("Vertical");
		base.ApplyMoveActor();
    }

    protected override void ApplyAttack()
    {
        var currentWeapon = arsenal?.GetCurrentWeapon();
		var isMeleeWeapon = currentTypeWeapon == TypeWeapon.MELEE;

		if (Input.GetMouseButton(0))
        {
            attackMode.StartAttackMode(isMeleeWeapon);
			currentWeapon.StartAttack(this, playerCamera.GetCursorPosition());
        }
        if (Input.GetMouseButtonUp(0))
        {
            currentWeapon.StopAttack();
        }
        attackMode.IncreaseCurrentTimeAttackMode(Time.deltaTime);
    }
	protected override void ApplyTargetPoint()
	{
        targetPoint = playerCamera.GetCursorPosition();
	}
	protected override void ChangeWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            attackMode.StopAttackMode();
			arsenal.ChangeWeapon(0);
		}
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
			arsenal.ChangeWeapon(1);
		}
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
			arsenal.ChangeWeapon(2);
		}
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
			arsenal.ChangeWeapon(3);
		}
        
        currentTypeWeapon = arsenal.GetCurrentWeapon().CurrentTypeWeapon;
	}
}
