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

	protected override void UpdateMovementActor()
	{
		RightMovementValue = Input.GetAxis("Horizontal");
		ForwardMovementValue = Input.GetAxis("Vertical");
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
		if (isMeleeWeapon && currentWeaponNumber == 0)
		{
			attackMode.DeactivateAttackMode();
		}
	}

	protected override void UpdateTargetPoint()
	{
        targetPoint = playerCamera.GetCursorPosition();
	}

    private void InputDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
			isDash = true;
		}
    }
}
