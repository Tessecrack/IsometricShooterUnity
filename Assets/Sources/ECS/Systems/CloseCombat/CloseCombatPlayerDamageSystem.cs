using Leopotam.EcsLite;
using UnityEngine;

public class CloseCombatPlayerDamageSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();

		var filterPlayer = world.Filter<PlayerComponent>()
			.Inc<CharacterComponent>()
			.Inc<CloseCombatComponent>()
			.Inc<EnablerComponent>()
			.Inc<DamageComponent>()
			.Inc<HitRangeComponent>()
			.End();

		var filterEnemy = world.Filter<EnemyComponent>()
			.Inc<CharacterComponent>()
			.Inc<EnablerComponent>()
			.Inc<HitMeComponent>()
			.End();

		var playerCloseCombats = world.GetPool<CloseCombatComponent>();
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

			ref var playerCloseCombat = ref playerCloseCombats.Get(entityPlayer);
			if (playerCloseCombat.closeCombat.AttackInProccess == false)
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

				if (playerCloseCombat.closeCombat.IsApplyDamage == true && enemyHit.isHitMe == false)
				{
					enemyHit.isHitMe = true;
					enemyHit.wasAppliedDamageMe = false;
					enemyHit.damageToMe = playerDamage.damage;
				}

				if (playerCloseCombat.closeCombat.IsApplyDamage == false)
				{
					enemyHit.isHitMe = false;
					enemyHit.wasAppliedDamageMe = false;
				}
			}
		}	
	}
}
