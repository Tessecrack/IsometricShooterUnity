using UnityEngine;


public class PlayerController : ActorController
{
    [SerializeField] private PlayerCamera playerCamera;

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

        actorVelocityVector = verticalMovementValue * initialActorForwardVector + horizontalMovementValue * initialActorRightVector;

        var vectorMovePlayer = new Vector3(actorVelocityVector.x * speed, 0.0f, actorVelocityVector.z * speed);
        characterController.Move(vectorMovePlayer * Time.fixedDeltaTime);
    }

    protected override void ApplyRotationActor()
    {
        var startPosition = this.transform.position;
        var endPosition = playerCamera.GetCursorPosition();

        var direction = Vector3.RotateTowards(this.transform.forward, endPosition - startPosition, Time.fixedDeltaTime * 20, 0.0f);
        this.transform.rotation = Quaternion.LookRotation(direction);
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
}
