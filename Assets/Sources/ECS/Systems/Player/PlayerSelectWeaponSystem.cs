using Leopotam.Ecs;
using UnityEngine;

public class PlayerSelectWeaponSystem : IEcsRunSystem
{
	EcsFilter<InputEventComponent, WeaponComponent> filter;
	private StaticData staticData;
	public void Run()
	{
		foreach(var i in filter)
		{
			ref var inputComponent = ref filter.Get1(i);
			ref var weaponComponent = ref filter.Get2(i);

			int amountWeapons = staticData.Weapons.WeaponsPrefabs.Count;

			if (inputComponent.selectedNumberWeapon < amountWeapons
				&& inputComponent.selectedNumberWeapon != weaponComponent.currentNumberWeapon)
			{
				Object.Destroy(weaponComponent.weaponInstance);
				weaponComponent.weaponInstance = null;

				weaponComponent.weaponInstance = Object.Instantiate(staticData.Weapons.WeaponsPrefabs[inputComponent.selectedNumberWeapon],
					weaponComponent.pointSpawnWeapon, false);
				weaponComponent.weapon = weaponComponent.weaponInstance.GetComponent<Weapon>();
				weaponComponent.typeWeapon = weaponComponent.weapon.TypeWeapon;
				weaponComponent.currentNumberWeapon = inputComponent.selectedNumberWeapon;
			}
		}
	}
}
