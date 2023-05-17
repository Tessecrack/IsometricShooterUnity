using Leopotam.EcsLite;
using UnityEngine;

public class CharacterSelectWeaponSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		EcsWorld world = systems.GetWorld();
		EcsFilter filter = world.Filter<CharacterEventsComponent>()
			.Inc<WeaponComponent>()
			.End();

		EcsPool<CharacterEventsComponent> inputs = world.GetPool<CharacterEventsComponent>();
		EcsPool<WeaponComponent> weapons = world.GetPool<WeaponComponent>();
		var sharedData = systems.GetShared<SharedData>();

		foreach(int entity in filter)
		{
			ref var inputComponent = ref inputs.Get(entity);
			ref var weaponComponent = ref weapons.Get(entity);

			int amountWeapons = sharedData.StaticData.Weapons.WeaponsPrefabs.Count;

			if (inputComponent.selectedNumberWeapon < amountWeapons
				&& inputComponent.selectedNumberWeapon != weaponComponent.currentNumberWeapon)
			{
				weaponComponent.weaponsPool.Disable(weaponComponent.currentNumberWeapon);	

				var currentInstanceWeapon = weaponComponent.weaponsPool.Enable(inputComponent.selectedNumberWeapon);

				weaponComponent.weaponInstance = currentInstanceWeapon.instance;
				weaponComponent.weapon = currentInstanceWeapon.weapon;

				weaponComponent.typeWeapon = weaponComponent.weapon.GetTypeWeapon();
				weaponComponent.currentNumberWeapon = inputComponent.selectedNumberWeapon;
			}
		}
	}
}
