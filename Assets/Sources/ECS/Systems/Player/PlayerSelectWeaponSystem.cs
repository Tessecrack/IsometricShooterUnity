using Leopotam.EcsLite;
using UnityEngine;

public class PlayerSelectWeaponSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		EcsWorld world = systems.GetWorld();
		EcsFilter filter = world.Filter<InputEventComponent>()
			.Inc<WeaponComponent>()
			.End();

		EcsPool<InputEventComponent> inputs = world.GetPool<InputEventComponent>();
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
				Object.Destroy(weaponComponent.weaponInstance);
				weaponComponent.weaponInstance = null;

				weaponComponent.weaponInstance = Object
					.Instantiate(sharedData.StaticData.Weapons.WeaponsPrefabs[inputComponent.selectedNumberWeapon],
					weaponComponent.pointSpawnWeapon, false);

				weaponComponent.weapon = weaponComponent.weaponInstance.GetComponent<Weapon>();
				weaponComponent.typeWeapon = weaponComponent.weapon.GetTypeWeapon();
				weaponComponent.currentNumberWeapon = inputComponent.selectedNumberWeapon;
			}
		}
	}
}
