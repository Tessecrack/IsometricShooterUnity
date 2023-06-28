using Leopotam.EcsLite;
using Mono.Cecil.Cil;

public class CharacterSelectWeaponSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		EcsWorld world = systems.GetWorld();
		EcsFilter filter = world.Filter<CharacterEventsComponent>()
			.Inc<ArsenalComponent>()
			.Inc<WeaponComponent>()
			.Inc<CharacterComponent>()
			.Inc<EnablerComponent>()
			.End();

		EcsPool<CharacterEventsComponent> eventComponents = world.GetPool<CharacterEventsComponent>();
		EcsPool<ArsenalComponent> arsenals = world.GetPool<ArsenalComponent>();
		EcsPool<WeaponComponent> weapons = world.GetPool<WeaponComponent>();
		EcsPool<CharacterComponent> characterComponents = world.GetPool<CharacterComponent>();
		EcsPool<StateAttackComponent> states = world.GetPool<StateAttackComponent>();

		var enablers = world.GetPool<EnablerComponent>();

		foreach(int entity in filter)
		{
			ref var enabler = ref enablers.Get(entity);
			if (!enabler.isEnabled)
			{
				continue;
			}

			ref var eventComponent = ref eventComponents.Get(entity);
			ref var arsenal = ref arsenals.Get(entity);
			ref var characterComponent = ref characterComponents.Get(entity);
			ref var state = ref states.Get(entity);
			ref var weaponComponent = ref weapons.Get(entity);

			if (eventComponent.selectedNumberWeapon != arsenal.currentNumberWeapon
				&& !state.isMeleeAttack)
			{
				arsenal.arsenal.HideWeapon(arsenal.currentNumberWeapon);	

				var currentInstanceWeapon = arsenal.arsenal.GetWeapon(eventComponent.selectedNumberWeapon);

				arsenal.currentNumberWeapon = eventComponent.selectedNumberWeapon;

				weaponComponent.weapon = currentInstanceWeapon.weapon;
			}
		}
	}
}
