using System.Collections;
using System.Collections.Generic;
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
	}

	protected override void UpdateAttackMode()
	{
		if (agent.IsPlayerFounded)
		{
			attackMode.Enable();
		}
		else
		{
			attackMode.Disable();
		}
	}

	protected override void UpdateTargetPoint()
	{
		actorMovement.UpdateTargetPoint(agent.GetTargetPosition());
	}

	protected override void StartAttack(Vector3 target)
	{
		if (attackMode.IsNeedAttack)
		{
			turretWeapon.StartAttack(this, agent.GetTargetPosition());
		}
	}

	protected override void StopAttack()
	{
		if (!attackMode.IsNeedAttack)
		{
			turretWeapon.StopAttack();
		}
	}

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
