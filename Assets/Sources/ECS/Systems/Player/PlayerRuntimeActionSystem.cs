using Leopotam.EcsLite;
using UnityEngine;

public class PlayerRuntimeActionSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();
		var filter = world.Filter<PlayerComponent>()
			.Inc<MovableComponent>()
			.Inc<WeaponComponent>()
			.End();

		var sharedData = systems.GetShared<SharedData>();
		var movableComponent = world.GetPool<MovableComponent>();
		var weapons = world.GetPool<WeaponComponent>();
		foreach(var entity in filter)
		{
			ref var movable = ref movableComponent.Get(entity);
			ref var weapon = ref weapons.Get(entity);
			sharedData.RuntimeData.PlayerPosition = movable.transform.position;
			sharedData.RuntimeData.OwnerCameraTransform = movable.transform.position;

			if (weapon.weapon != null && weapon.weapon.TypeWeapon == TypeWeapon.MELEE)
			{
				sharedData.RuntimeData.PlayerActions.SetDamageCloseCombat(weapon.weapon.Damage);
			}
		}
	}
}
