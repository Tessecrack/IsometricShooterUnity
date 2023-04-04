using UnityEditor;
using UnityEngine;


public class PlayerController : ActorController
{
    [SerializeField] private PlayerCamera playerCamera;
	private readonly int speed = 6;
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
    }

	protected override void UpdateMovementActor()
	{
		actorMovement.SetForwardMovementValue(Input.GetAxis("Vertical"));
		actorMovement.SetRightMovementValue(Input.GetAxis("Horizontal"));
		InputDash();
	}
	protected override void UpdateAttackMode()
	{
		if (Input.GetMouseButton(0))
		{
			attackMode.SetStartAttack();
		}
		if (Input.GetMouseButtonUp(0))
		{
			attackMode.SetStopAttack();
		}
	}
	protected override void UpdateWeapon()
	{
		bool isMeleeWeapon = false;
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			isMeleeWeapon = true;
			currentWeaponNumber = 0;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			currentWeaponNumber = 1;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			currentWeaponNumber = 2;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			currentWeaponNumber = 3;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha5))
		{
			currentWeaponNumber = 4;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha6))
		{
			currentWeaponNumber = 5;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha7))
		{
			currentWeaponNumber = 6;
		}
		if (isMeleeWeapon && currentWeaponNumber == 0)
		{
			attackMode.DeactivateAttackMode();
		}
	}

	protected override void UpdateTargetPoint()
	{
        actorMovement.UpdateTargetPoint(playerCamera.GetCursorPosition());
	}

    private void InputDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
			actorMovement.IsDash = true;
		}
    }
}
