using UnityEngine;

public class PlayerController : ActorController
{
    [SerializeField] private PlayerCamera playerCamera;

	private readonly int speed = 6;

	private int defaultNumberWeapon = 1;

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
		actorMovement.InitInitialOptions(speed, playerCamera.ForwardVector, playerCamera.RightVector);
		this.gameObject.layer = playerLayerMask;
    	}

	protected override void UpdateMovementActor()
	{
		actorMovement.SetLocalForwardMovementValue(Input.GetAxis("Vertical"));
		actorMovement.SetLocalRightMovementValue(Input.GetAxis("Horizontal"));
		InputDash();
	}

	protected override void UpdateAttackMode()
	{
		if (Input.GetMouseButton(0))
		{
			OnStartAttack?.Invoke();
		}
		if (Input.GetMouseButtonUp(0))
		{
			OnStopAttack?.Invoke();
		}
		attackMode.UpdateTimeAttackMode(Time.deltaTime);
	}

	protected override void UpdateWeapon()
	{
		bool isChanged = false;
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			isChanged = arsenal.ChangeWeapon(0);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			isChanged = arsenal.ChangeWeapon(1);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			isChanged = arsenal.ChangeWeapon(2);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			isChanged = arsenal.ChangeWeapon(3);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha5))
		{
			isChanged = arsenal.ChangeWeapon(4);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha6))
		{
			isChanged = arsenal.ChangeWeapon(5);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha7))
		{
			isChanged = arsenal.ChangeWeapon(6);
		}

		CurrentTypeWeapon = arsenal.GetCurrentWeapon().CurrentTypeWeapon;

		if (isChanged && CurrentTypeWeapon == TypeWeapon.MELEE)
		{
			attackMode.ForceDisable();
		}
	}

	protected override void UpdateTargetPoint()
	{
		actorMovement.UpdateTargetPoint(playerCamera.GetCursorPosition());
	}

	protected override void SetDefaultWeapon()
	{
		arsenal.SetInitialWeapon(defaultNumberWeapon);
	}

	private void InputDash()
	{
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			OnDash?.Invoke();
		}
    }
}
