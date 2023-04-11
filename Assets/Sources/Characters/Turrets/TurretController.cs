using UnityEngine;

public class TurretController : ActorController
{
	private AIController agent;
	private FastTurretWeapon turretWeapon;

	protected override void InitController()
	{
		attackMode = new AttackMode();
		actorMovement = new ActorMovement();
		health = new ActorHealth();

		turretWeapon = GetComponent<FastTurretWeapon>();

		var player = FindObjectOfType<PlayerController>();
		agent = AIController.InitAIController(this.transform, player.transform, player.gameObject.layer);

		OnStartAttack += StartAttack;
		OnStopAttack += StopAttack;
		OnTakeDamage += health.TakeDamage;
	}

	protected override void UpdateAttackMode()
	{
		if (agent.CanAttack)
		{
			OnStartAttack?.Invoke();
		}
		else
		{
			OnStopAttack?.Invoke();
		}
	}

	protected override void UpdateTargetPoint()
	{
		actorMovement.UpdateTargetPoint(agent.GetTargetPosition());
	}

	protected override void ApplyRotationActor()
	{
		var direction = actorMovement.ActorVelocityVector;
		if (agent.IsPlayerFounded)
		{
			direction = actorMovement.GetTargetPoint() - this.transform.position;
		}
		this.transform.forward = actorMovement.Rotate(this.transform.forward,
			direction, Time.fixedDeltaTime);
	}

	public override Weapon GetCurrentWeapon() => turretWeapon;

	protected override void UpdateAnimation()
	{

	}

	protected override void UpdateMovementActor()
	{
		
	}

	protected override void UpdateWeapon()
	{
		
	}

	protected override void ApplyMovementActor()
	{
		
	}

	protected override void SetDefaultWeapon()
	{

	}
}
