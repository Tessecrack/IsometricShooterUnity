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
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			currentNumberWeapon = 0;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			currentNumberWeapon = 1;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			currentNumberWeapon = 2;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			currentNumberWeapon = 3;
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
            Debug.Log("DASH");
        }
    }
}
