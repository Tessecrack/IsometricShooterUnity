using Leopotam.EcsLite;
using UnityEngine;

public class CharacterRigSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();

		var entities = world.Filter<CharacterRigComponent>()
			.Inc<CharacterComponent>()
			.Inc<CharacterStateAttackComponent>()
			.Inc<MovableComponent>()
			.Inc<WeaponComponent>()
			.End();

		var sharedData = systems.GetShared<SharedData>();
		var cursorPosition = sharedData.RuntimeData.CursorPosition;

		var characterRigComponents = world.GetPool<CharacterRigComponent>();
		var characterComponents = world.GetPool<CharacterComponent>();
		var stateComponents = world.GetPool<CharacterStateAttackComponent>();
		var movableComponents = world.GetPool<MovableComponent>();
		var weaponComponents = world.GetPool<WeaponComponent>();

		foreach(var entity in entities)
		{
			ref var characterRigComponent = ref characterRigComponents.Get(entity);
			ref var characterComponent = ref characterComponents.Get(entity);
			ref var stateComponent = ref stateComponents.Get(entity);
			ref var movableComponent = ref movableComponents.Get(entity);
			ref var weaponComponent = ref weaponComponents.Get(entity);

			var characterTransform = characterComponent.characterTransform;

			var angle = Vector3.Angle(characterTransform.forward, cursorPosition - characterTransform.position);
			if (stateComponent.characterState == CharacterState.Rest 
				&& movableComponent.velocity.magnitude == 0 && angle < 90)
			{
				characterRigComponent.characterRigController.SetTargetHeadChestRig(cursorPosition);
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
