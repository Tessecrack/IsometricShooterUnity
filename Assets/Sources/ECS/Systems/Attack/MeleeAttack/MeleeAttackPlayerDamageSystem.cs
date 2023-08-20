using Leopotam.EcsLite;
using UnityEngine;

public class MeleeAttackPlayerDamageSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();

		var filterPlayer = world.Filter<PlayerComponent>()
			.Inc<CharacterComponent>()
			.Inc<BaseAttackComponent>()
			.Inc<EnablerComponent>()
			.Inc<DamageComponent>()
			.Inc<HitRangeComponent>()
			.End();

		var filterEnemy = world.Filter<EnemyComponent>()
			.Inc<CharacterComponent>()
			.Inc<EnablerComponent>()
			.Inc<HitMeComponent>()
			.End();

		var playerAttackEvents = world.GetPool<BaseAttackComponent>();
		var playerEnablers = world.GetPool<EnablerComponent>();
		var playerHitRanges = world.GetPool<HitRangeComponent>();
		var playerCharacters = world.GetPool<CharacterComponent>();
		var playerHitDamages = world.GetPool<DamageComponent>();

		var enemyCharacters = world.GetPool<CharacterComponent>();
		var enemyEnablers = world.GetPool<EnablerComponent>();
		var enemyHits = world.GetPool<HitMeComponent>();

		foreach(var entityPlayer in filterPlayer)
		{
			ref var playerEnabler = ref playerEnablers.Get(entityPlayer);
			if (playerEnabler.isEnabled == false)
			{
				continue;
			}

			ref var playerAttackEvent = ref playerAttackEvents.Get(entityPlayer);
			if (playerAttackEvent.baseAttack.IsAttackInProcess == false
				|| playerAttackEvent.baseAttack.IsEventAttack == false)
			{
				continue;
			}
			
			ref var playerRangeHit = ref playerHitRanges.Get(entityPlayer);
			ref var playerCharacter = ref playerCharacters.Get(entityPlayer);
			ref var playerDamage = ref playerHitDamages.Get(entityPlayer);

			var positionPlayer = playerCharacter.characterTransform.position;

			foreach(var entityEnemy in filterEnemy)
			{
				ref var enemyEnabler = ref enemyEnablers.Get(entityEnemy);
				if (enemyEnabler.isEnabled == false)
				{
					continue;
				}

				ref var enemyCharacter = ref enemyCharacters.Get(entityEnemy);
				var positionEnemy = enemyCharacter.characterTransform.position;
				var distanceToEnemy = Vector3.Distance(positionPlayer, positionEnemy);
				
				if (playerRangeHit.rangeHit < distanceToEnemy)
				{
					continue;
				}

				ref var enemyHit = ref enemyHits.Get(entityEnemy);

				if (playerAttackEvent.baseAttack.IsApplyDamage == true && enemyHit.isHitMe == false)
				{
					enemyHit.isHitMe = true;
					enemyHit.wasAppliedDamageMe = false;
					enemyHit.damageToMe = playerDamage.damage;
				}

				if (playerAttackEvent.baseAttack.IsApplyDamage == false)
				{
					enemyHit.isHitMe = false;
					enemyHit.wasAppliedDamageMe = false;
				}
			}
		}	
	}
}
