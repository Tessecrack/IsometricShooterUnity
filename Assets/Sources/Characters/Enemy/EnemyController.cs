using UnityEngine;

public class EnemyController : ActorController
{
	private AIController agent;
	public TypeEnemy TypeEnemy { get; protected set; }

	private readonly int defaultNumberWeapon = 0;

	protected override void InitController()
	{
		base.InitController();
		var player = FindObjectOfType<PlayerController>();

		agent = AIController.InitAIController(this.transform, player.transform, player.gameObject.layer);
	}

	protected override void ApplyAttack()
	{
		if (agent.IsPlayerFounded)
		{
			attackMode.StartAttack(actorMovement.GetTargetPoint());
		}
		else
		{
			attackMode.StopAttack();
			attackMode.DeactivateAttackMode();
		}
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
		
	}

	protected override void UpdateWeapon()
	{
		CurrentTypeWeapon = arsenal.GetCurrentWeapon().CurrentTypeWeapon;
	}

	protected override void SetDefaultWeapon()
	{
		arsenal.SetInitialWeapon(defaultNumberWeapon);
	}
}
