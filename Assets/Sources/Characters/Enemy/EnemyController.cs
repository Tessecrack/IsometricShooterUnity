using UnityEngine;

public class EnemyController : ActorController
{
	private AIController agent;

	private readonly int defaultNumberWeapon = 0;

	protected override void InitController()
	{
		base.InitController();
		var player = FindObjectOfType<PlayerController>();
		agent = AIController.InitAIController(this.transform, player.transform, player.gameObject.layer);
		OnTakeDamage += agent.TakeDamage;
	}

	protected override void UpdateTargetPoint()
	{
		actorMovement.UpdateTargetPoint(agent.GetTargetPosition());
	}

	protected override void UpdateMovementActor()
	{
		
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

	protected override void UpdateWeapon()
	{

	}

	protected override void SetDefaultWeapon()
	{
		arsenal.SetInitialWeapon(defaultNumberWeapon);
		CurrentTypeWeapon = arsenal.GetCurrentWeapon().CurrentTypeWeapon;
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
}
