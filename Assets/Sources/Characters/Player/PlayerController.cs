using System.Collections;
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
        speed = 5;
    }

    protected override void ApplyMoveActor()
    {
        horizontalMovementValue = Input.GetAxis("Horizontal");
        verticalMovementValue = Input.GetAxis("Vertical");
        base.ApplyMoveActor();
    }

    protected override void ApplyAttack()
    {
        var currentWeapon = arsenal?.GetCurrentWeapon();
        if (Input.GetMouseButton(0))
        {
            isAttackMode = true;
            currentTimeAttackMode = 0.0f;
            //currentWeapon.StartShoot(this, playerCamera.GetCursorPosition());
        }
        if (Input.GetMouseButtonUp(0))
        {
            //currentWeapon.StopShoot();
        }
    }
	protected override void ApplyTargetPoint()
	{
        targetPoint = playerCamera.GetCursorPosition();
	}
	protected override void ChangeWeapon()
    {
        if (arsenal == null)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            arsenal.ChangeWeapon(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            arsenal.ChangeWeapon(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            arsenal.ChangeWeapon(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            arsenal.ChangeWeapon(4);
        }
    }
}
