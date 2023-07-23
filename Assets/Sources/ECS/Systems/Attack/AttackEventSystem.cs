using Leopotam.EcsLite;
using UnityEngine;

public class AttackEventSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();
		var filter = world.Filter<CharacterEventsComponent>()
			.Inc<AttackComponent>()
			.Inc<CharacterComponent>()
			.Inc<TargetComponent>()
			.Inc<EnablerComponent>()
			.Inc<WeaponTypeComponent>()
			.End();

		var characterEvents = world.GetPool<CharacterEventsComponent>();
		var attacks = world.GetPool<AttackComponent>();
		var characters = world.GetPool<CharacterComponent>();
		var targets = world.GetPool<TargetComponent>();
		var enablers = world.GetPool<EnablerComponent>();
		var weaponTypes = world.GetPool<WeaponTypeComponent>();

		foreach (int entity in filter)
		{
			ref var enablerComponent = ref enablers.Get(entity);
			if (enablerComponent.isEnabled == false)
			{
				continue;
			}
			ref var characterEvent = ref characterEvents.Get(entity);
			ref var attackComponent = ref attacks.Get(entity);
			ref var characterComponent = ref characters.Get(entity);
			ref var targetComponent = ref targets.Get(entity);
			ref var weaponType = ref weaponTypes.Get(entity);

			attackComponent.isStartAttack = characterEvent.isStartAttack;
			
			attackComponent.isStopAttack = characterEvent.isStopAttack;

			attackComponent.attackerTransform = characterComponent.characterTransform;

			attackComponent.typeAttack = weaponType.typeWeapon == TypeWeapon.MELEE ? TypeAttack.Melee : TypeAttack.Range;
		}
	}
}
