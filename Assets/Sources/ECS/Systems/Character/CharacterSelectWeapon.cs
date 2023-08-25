using Leopotam.EcsLite;

public class CharacterSelectWeaponSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		EcsWorld world = systems.GetWorld();
		EcsFilter filter = world.Filter<CharacterEventsComponent>()
			.Inc<ArsenalComponent>()
			.Inc<WeaponComponent>()
			.Inc<CharacterComponent>()
			.Inc<DamageComponent>()
			.Inc<EnablerComponent>()
			.Inc<BaseAttackComponent>()
			.End();

		EcsPool<CharacterEventsComponent> eventComponents = world.GetPool<CharacterEventsComponent>();
		EcsPool<ArsenalComponent> arsenals = world.GetPool<ArsenalComponent>();
		EcsPool<WeaponComponent> weapons = world.GetPool<WeaponComponent>();
		EcsPool<CharacterComponent> characterComponents = world.GetPool<CharacterComponent>();
		EcsPool<WeaponTypeComponent> weaponTypes = world.GetPool<WeaponTypeComponent>();
		EcsPool<BaseAttackComponent> baseAttacks = world.GetPool<BaseAttackComponent>();

		var enablers = world.GetPool<EnablerComponent>();
		var damageComponents = world.GetPool<DamageComponent>();

		foreach (int entity in filter)
		{
			ref var enabler = ref enablers.Get(entity);
			if (enabler.isEnabled == false)
			{
				continue;
			}

			ref var eventComponent = ref eventComponents.Get(entity);
			ref var arsenal = ref arsenals.Get(entity);
			if (eventComponent.selectedNumberWeapon == arsenal.currentNumberWeapon)
			{
				continue;
			}

			ref var characterComponent = ref characterComponents.Get(entity);
			ref var weaponComponent = ref weapons.Get(entity);
			ref var damageComponent = ref damageComponents.Get(entity);
			ref var weaponTypeComponent = ref weaponTypes.Get(entity);

			ref var baseAttack = ref baseAttacks.Get(entity);

			arsenal.arsenal.HideWeapon(arsenal.currentNumberWeapon);
			var currentWeapon = arsenal.arsenal.GetWeapon(eventComponent.selectedNumberWeapon);
			arsenal.currentNumberWeapon = eventComponent.selectedNumberWeapon;
			weaponComponent.weapon = currentWeapon;
			baseAttack.baseAttack = currentWeapon.BaseAttack;

			damageComponent.damage = weaponComponent.weapon.Damage;
			weaponTypeComponent.typeWeapon = weaponComponent.weapon.TypeWeapon;
		}
	}
}
