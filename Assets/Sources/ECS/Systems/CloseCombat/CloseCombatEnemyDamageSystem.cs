using Leopotam.EcsLite;
using System.Diagnostics;

public class CloseCombatEnemyDamageSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();

		var filterEnemy = world.Filter<AIEnemyComponent>()
			.Inc<CloseCombatComponent>()
			.Inc<EnablerComponent>()
			.Inc<DamageComponent>()
			.End();

		var filterPlayer = world.Filter<PlayerComponent>()
			.Inc<HitMeComponent>()
			.Inc<EnablerComponent>()
			.Inc<CharacterComponent>()
			.End();

		var enemiesAI = world.GetPool<AIEnemyComponent>();
		var enemyCombats = world.GetPool<CloseCombatComponent>();
		var enemyEnablers = world.GetPool<EnablerComponent>();
		var enemyDamages = world.GetPool<DamageComponent>();

		var playerHits = world.GetPool<HitMeComponent>();
		var playerCharacters = world.GetPool<CharacterComponent>();
		var playerEnablers = world.GetPool<EnablerComponent>();

		foreach (var enemyEntity in filterEnemy)
		{
			ref var enablerEnemy = ref enemyEnablers.Get(enemyEntity);
			if (enablerEnemy.isEnabled == false)
			{
				continue;
			}
			ref var enemyCombat = ref enemyCombats.Get(enemyEntity);
			if (enemyCombat.closeCombat.AttackInProccess == false)
			{
				continue;
			}
			ref var enemyAI = ref enemiesAI.Get(enemyEntity);
			ref var enemyDamage = ref enemyDamages.Get(enemyEntity);
			foreach (var playerEntity in filterPlayer)
			{
				ref var enablerPlayer = ref playerEnablers.Get(playerEntity);
				if (enablerPlayer.isEnabled == false) 
				{ 
					continue; 
				}
				ref var playerCharacter = ref playerCharacters.Get(playerEntity);
				if (enemyAI.enemyAgent.CanAttack(playerCharacter.characterTransform.position) == false)
				{
					continue;
				}
				ref var hitMeComponent = ref playerHits.Get(playerEntity);

				if (enemyCombat.closeCombat.IsApplyDamage == true && hitMeComponent.isHitMe == false)
				{
					hitMeComponent.isHitMe = true;
					hitMeComponent.wasAppliedDamageMe = false;
					hitMeComponent.damageToMe = enemyDamage.damage;
				}

				if (enemyCombat.closeCombat.IsApplyDamage == false)
				{
					hitMeComponent.isHitMe = false;
					hitMeComponent.wasAppliedDamageMe = false;
				}
			}
		}
	}
}
