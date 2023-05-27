using Leopotam.EcsLite;

public class CharacterSelectWeaponSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		EcsWorld world = systems.GetWorld();
		EcsFilter filter = world.Filter<CharacterEventsComponent>()
			.Inc<WeaponComponent>()
			.Inc<CharacterComponent>()
			.End();

		EcsPool<CharacterEventsComponent> eventComponents = world.GetPool<CharacterEventsComponent>();
		EcsPool<WeaponComponent> weapons = world.GetPool<WeaponComponent>();
		EcsPool<CharacterComponent> characterComponents = world.GetPool<CharacterComponent>();

		var sharedData = systems.GetShared<SharedData>();

		foreach(int entity in filter)
		{
			ref var eventComponent = ref eventComponents.Get(entity);
			ref var weaponComponent = ref weapons.Get(entity);
			ref var characterComponent = ref characterComponents.Get(entity);

			int amountWeapons = sharedData.StaticData.Weapons.WeaponsPrefabs.Count;

			if (eventComponent.selectedNumberWeapon < amountWeapons
				&& eventComponent.selectedNumberWeapon != weaponComponent.currentNumberWeapon)
			{
				weaponComponent.weaponsPool.Disable(weaponComponent.currentNumberWeapon);	

				var currentInstanceWeapon = weaponComponent.weaponsPool.Enable(eventComponent.selectedNumberWeapon);
				
				weaponComponent.weaponInstance = currentInstanceWeapon.instance;
				weaponComponent.weapon = currentInstanceWeapon.weapon;
				weaponComponent.typeWeapon = weaponComponent.weapon.GetTypeWeapon();
				weaponComponent.currentNumberWeapon = eventComponent.selectedNumberWeapon;
			}
		}
	}
}
