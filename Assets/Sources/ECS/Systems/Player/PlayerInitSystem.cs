using Leopotam.Ecs;
using UnityEngine;

public class PlayerInitSystem : IEcsInitSystem
{
	private EcsWorld ecsWorld;
	private StaticData staticData;
	private SceneData sceneData;
	private RuntimeData runtimeData;

	public void Init()
	{
		var entityPlayer = ecsWorld.NewEntity();

		ref var characterComponent = ref entityPlayer.Get<CharacterComponent>();
		ref var inputComponent = ref entityPlayer.Get<InputEventComponent>();
		ref var movableComponent = ref entityPlayer.Get<MovableComponent>();
		ref var animatorComponent = ref entityPlayer.Get<AnimatorComponent>();
		ref var weaponComponent = ref entityPlayer.Get<WeaponComponent>();
		ref var attackComponent = ref entityPlayer.Get<AttackComponent>(); 

		GameObject player = Object.Instantiate(staticData.PlayerPrefab, sceneData.playerSpawnPoint.position, Quaternion.identity);

		runtimeData.OwnerCameraTransform = player.transform;
		characterComponent.currentPosition = player.transform;

		var characterSettings = player.GetComponent<CharacterSettings>();
		player.transform.forward = staticData.GlobalForwardVector;

		animatorComponent.animator = player.GetComponent<Animator>();
		characterComponent.characterController = player.GetComponent<CharacterController>();

		movableComponent.transform = player.transform;

		movableComponent.speedMove = characterSettings.CharacterSpeed;
		weaponComponent.pointSpawnWeapon = characterSettings.PointSpawnWeapon;

		weaponComponent.currentNumberWeapon = -1;
		inputComponent.selectedNumberWeapon = 0;
	}
}
