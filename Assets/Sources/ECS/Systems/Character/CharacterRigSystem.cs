using Leopotam.EcsLite;
using UnityEngine;

public class CharacterRigSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();

		var entities = world.Filter<CharacterRigComponent>()
			.Inc<CharacterComponent>()
			.Inc<StateAttackComponent>()
			.Inc<MovableComponent>()
			.Inc<WeaponComponent>()
			.Inc<TargetComponent>()
			.End();

		var sharedData = systems.GetShared<SharedData>();

		var characterRigComponents = world.GetPool<CharacterRigComponent>();
		var characterComponents = world.GetPool<CharacterComponent>();
		var stateComponents = world.GetPool<StateAttackComponent>();
		var movableComponents = world.GetPool<MovableComponent>();
		var weaponComponents = world.GetPool<WeaponComponent>();
		var targetComponents = world.GetPool<TargetComponent>();

		foreach(var entity in entities)
		{
			ref var characterRigComponent = ref characterRigComponents.Get(entity);
			ref var characterComponent = ref characterComponents.Get(entity);
			ref var stateComponent = ref stateComponents.Get(entity);
			ref var movableComponent = ref movableComponents.Get(entity);
			ref var weaponComponent = ref weaponComponents.Get(entity);
			ref var targetComponent = ref targetComponents.Get(entity);

			var characterTransform = characterComponent.characterTransform;

			var angle = Vector3.Angle(characterTransform.forward, targetComponent.target - characterTransform.position);
			if (stateComponent.state == CharacterState.Rest 
				&& movableComponent.velocity.magnitude == 0 && angle < 90)
			{
				characterRigComponent.characterRigController.SetTargetHeadChestRig(targetComponent.target);
			}
			else
			{
				characterRigComponent.characterRigController.ResetRigHeadChest(true);
			}

			if (weaponComponent.typeWeapon == TypeWeapon.HEAVY)
			{
				characterRigComponent.characterRigController.SetTargetLeftArm(weaponComponent.weapon.GetAdditionalGrip().position);
			}
			else
			{
				characterRigComponent.characterRigController.ResetRigArms();
			}
		}
	}
}
