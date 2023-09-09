using Leopotam.EcsLite;

public class CharacterAligmentWeaponSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();
		var entities = world.Filter<CharacterComponent>()
			.Inc<WeaponComponent>()
			.Inc<EnablerComponent>()
			.Inc<WeaponSpawnPointComponent>()
			.Inc<AimStateComponent>()
			.End();

		var characterComponents = world.GetPool<CharacterComponent>();
		var weaponComponents = world.GetPool<WeaponComponent>();
		var enablerComponents = world.GetPool<EnablerComponent>();
		var weaponSpawnPoints = world.GetPool<WeaponSpawnPointComponent>();
		var aimStates = world.GetPool<AimStateComponent>();

		foreach (var entity in entities)
		{
			ref var enabler = ref enablerComponents.Get(entity);
			if (enabler.isEnabled == false)
			{
				continue;
			}
			ref var aimState = ref aimStates.Get(entity);
			if (aimState.aimState == AimState.NO_AIM)
			{
				continue;
			}
			ref var weaponComponent = ref weaponComponents.Get(entity);
			if (weaponComponent.weapon.TypeWeapon == TypeWeapon.MELEE)
			{
				continue;
			}

			ref var pointSpawnWeapon = ref weaponSpawnPoints.Get(entity);
			ref var characterComponent = ref characterComponents.Get(entity);
			pointSpawnWeapon.weaponSpawPoint.forward = characterComponent.characterTransform.forward;
		}
	}
}