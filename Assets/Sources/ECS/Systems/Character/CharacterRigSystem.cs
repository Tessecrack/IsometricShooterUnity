using Leopotam.EcsLite;
using UnityEngine;

public class CharacterRigSystem : IEcsRunSystem
{
	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();

		var entities = world.Filter<CharacterRigComponent>()
			.Inc<CharacterComponent>()
			.Inc<CharacterStateComponent>()
			.Inc<AimStateComponent>()
			.Inc<MovableComponent>()
			.Inc<WeaponComponent>()
			.Inc<TargetComponent>()
			.Inc<EnablerComponent>()
			.Inc<VelocityComponent>()
			.Inc<BaseAttackComponent>()
			.End();

		var characterRigComponents = world.GetPool<CharacterRigComponent>();
		var characterComponents = world.GetPool<CharacterComponent>();
		var characterStates = world.GetPool<CharacterStateComponent>();
		var aimStates = world.GetPool<AimStateComponent>();
		var movableComponents = world.GetPool<MovableComponent>();
		var weaponComponents = world.GetPool<WeaponComponent>();
		var targetComponents = world.GetPool<TargetComponent>();
		var enablers = world.GetPool<EnablerComponent>();
		var velocityComponents = world.GetPool<VelocityComponent>();
		var baseAttackComponents = world.GetPool<BaseAttackComponent>();

		foreach(var entity in entities)
		{
			ref var enabler = ref enablers.Get(entity);
			ref var characterRigComponent = ref characterRigComponents.Get(entity);

			if (enabler.isEnabled == false)
			{
				characterRigComponent.characterRigController.ResetAll();
				continue;
			}

			ref var characterComponent = ref characterComponents.Get(entity);
			ref var aimState = ref aimStates.Get(entity);
			ref var movableComponent = ref movableComponents.Get(entity);
			ref var weaponComponent = ref weaponComponents.Get(entity);
			ref var targetComponent = ref targetComponents.Get(entity);
			ref var velocityComponent = ref velocityComponents.Get(entity);
			ref var characterState = ref characterStates.Get(entity);
			ref var baseAttack = ref baseAttackComponents.Get(entity);

			var characterTransform = characterComponent.characterTransform;

			var angle = Vector3.Angle(characterTransform.forward, targetComponent.target - characterTransform.position);

			if (characterState.characterState == CharacterState.DEATH)
			{
				characterRigComponent.characterRigController.ResetAll();
				continue;
			}
			
			if (characterState.characterState == CharacterState.IDLE 
				&& velocityComponent.velocity.magnitude == 0 && angle < 90 
				&& aimState.aimState == AimState.NO_AIM)
			{
				characterRigComponent.characterRigController.SetTargetHeadChestRig(targetComponent.target);
			}
			else
			{
				characterRigComponent.characterRigController.ResetRigHeadChest(true);
			}

			if (weaponComponent.weapon.TypeWeapon == TypeWeapon.HEAVY)
			{
				characterRigComponent.characterRigController.ResetRigRightArm();
				characterRigComponent.characterRigController.SetTargetLeftArm(weaponComponent.weapon.AdditionalGrip.position);
			}
			else if (weaponComponent.weapon.TypeWeapon == TypeWeapon.MELEE)
			{
				if (baseAttack.baseAttack.IsAttackInProcess)
				{
					characterRigComponent.characterRigController.ResetRigLeftArm();
					characterRigComponent.characterRigController.ResetRigRightArm();
				}
				else
				{
					characterRigComponent.characterRigController.ResetRigLeftArm();
					characterRigComponent.characterRigController.SetRigRightArm();
				}
			}
			else
			{
				characterRigComponent.characterRigController.ResetRigLeftArm();
				characterRigComponent.characterRigController.ResetRigRightArm();
			}
		}
	}
}
