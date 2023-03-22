using System.Collections;
using UnityEngine;


public class PlayerController : ActorController
{
    [SerializeField] private PlayerCamera playerCamera;

    private bool isAttack = false;

    protected override void InitController()
    {
        base.InitController();
        if (playerCamera != null)
        {
            playerCamera.SetOwner(this.gameObject);
        }
        speed = 6;
    }

    protected override void ApplyMoveActor()
    {
        horizontalMovementValue = Input.GetAxis("Horizontal");
        verticalMovementValue = Input.GetAxis("Vertical");
        base.ApplyMoveActor();
    }
    
    protected override Vector3 GetVelocity()
    {
        return isAttack ? playerCamera.GetCursorPosition() : this.transform.position + base.GetVelocity();
    }

    protected override void ApplyFire()
    {
        if (arsenal ==  null)
        {
            return;
        }
        var currentWeapon = arsenal.GetCurrentWeapon();
        if (currentWeapon == null)
        {
            return;
        }
        if (Input.GetMouseButton(0))
        {
            isAttack = true;
            //StartCoroutine(ChangeAttackPosition());
            currentWeapon.StartShoot(this, playerCamera.GetCursorPosition());
        }
        if (Input.GetMouseButtonUp(0))
        {
            currentWeapon.StopShoot();
        }
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

    private IEnumerator ChangeAttackPosition()
    {
        yield return new WaitForSeconds(3);
		isAttack = false;
    }
}
