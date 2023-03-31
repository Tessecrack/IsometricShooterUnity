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
		RightMovementValue = Input.GetAxis("Horizontal");
        ForwardMovementValue = Input.GetAxis("Vertical");
		base.ApplyMoveActor();
    }

    protected override void ApplyAttack()
    {
		if (Input.GetMouseButton(0))
        {
            attackMode.StartAttack(targetPoint);
        }
        if (Input.GetMouseButtonUp(0))
        {
            attackMode.StopAttack();
        }
        attackMode.IncreaseCurrentTimeAttackMode(Time.deltaTime);
    }
	protected override void ApplyTargetPoint()
	{
        targetPoint = playerCamera.GetCursorPosition();
	}
	protected override void ChangeWeapon()
    {
        bool isChangedWeapon = false;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
			isChangedWeapon = arsenal.ChangeWeapon(0);
		}
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
			isChangedWeapon = arsenal.ChangeWeapon(1);
		}
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
			isChangedWeapon = arsenal.ChangeWeapon(2);
		}
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
			isChangedWeapon = arsenal.ChangeWeapon(3);
		}

		CurrentTypeWeapon = arsenal.GetCurrentWeapon().CurrentTypeWeapon;

		if (isChangedWeapon && CurrentTypeWeapon == TypeWeapon.MELEE)
        {
            attackMode.DeactivateAttackMode();
        }
	}
}
