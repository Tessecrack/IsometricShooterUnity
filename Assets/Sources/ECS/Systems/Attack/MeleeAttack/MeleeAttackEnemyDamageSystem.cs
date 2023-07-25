using Leopotam.EcsLite;

public class MeleeAttackEnemyDamageSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();

		var filterEnemy = world.Filter<AIComponent>()
			.Inc<MeleeAttackComponent>()
			.Inc<EnablerComponent>()
			.Inc<DamageComponent>()
			.Inc<HitListComponent>()
			.End();

		var filterPlayer = world.Filter<PlayerComponent>()
			.Inc<HitMeComponent>()
			.Inc<EnablerComponent>()
			.Inc<CharacterComponent>()
			.End();

		var enemiesAI = world.GetPool<AIComponent>();
		var enemyMeleeAttacks = world.GetPool<MeleeAttackComponent>();
		var enemyEnablers = world.GetPool<EnablerComponent>();
		var enemyDamages = world.GetPool<DamageComponent>();
		var enemyHitLists = world.GetPool<HitListComponent>();

		var players = world.GetPool<PlayerComponent>();
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
			ref var enemyAttack = ref enemyMeleeAttacks.Get(enemyEntity);
			if (enemyAttack.meleeAttack.AttackInProcess == false)
			{
				continue;
			}
			ref var enemyAI = ref enemiesAI.Get(enemyEntity);
			ref var enemyDamage = ref enemyDamages.Get(enemyEntity);
			ref var enemyHitList = ref enemyHitLists.Get(enemyEntity);

			foreach (var playerEntity in filterPlayer)
			{
				ref var enablerPlayer = ref playerEnablers.Get(playerEntity);
				if (enablerPlayer.isEnabled == false) 
				{ 
					continue; 
				}
				ref var playerCharacter = ref playerCharacters.Get(playerEntity);
				if (enemyAI.aiAgent.CanMeleeAttack(playerCharacter.characterTransform.position) == false)
				{
					continue;
				}
				ref var hitMeComponent = ref playerHits.Get(playerEntity);
				ref var player = ref players.Get(playerEntity);

				if (enemyAttack.meleeAttack.IsApplyDamage == true 
					&& hitMeComponent.isHitMe == false
					&& enemyHitList.hitList.Contains(player.numberPlayer) == false)
				{
					hitMeComponent.isHitMe = true;
					hitMeComponent.wasAppliedDamageMe = false;
					hitMeComponent.damageToMe = enemyDamage.damage;
					enemyHitList.hitList.Add(player.numberPlayer);
				}

				if (enemyAttack.meleeAttack.IsApplyDamage == false)
				{
					enemyHitList.hitList.Clear();
					hitMeComponent.isHitMe = false;
					hitMeComponent.wasAppliedDamageMe = false;
				}
			}
		}
	}
}
