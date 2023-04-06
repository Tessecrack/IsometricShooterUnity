using Assets.Sources.Characters;
using System.Collections;
using UnityEngine;

public class EnemyController : ActorController
{
	public TypeEnemy TypeEnemy { get; protected set; }

	protected PlayerController Target { get; private set; }

	private readonly int defaultNumberWeapon = 0;

	protected override void ApplyAttack()
	{
		
	}

	protected override void UpdateTargetPoint()
	{

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
