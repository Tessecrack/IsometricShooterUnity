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
		ref var inputComponent = ref entityPlayer.Get<InputMovementComponent>();
		ref var movableComponent = ref entityPlayer.Get<MovableComponent>();
		ref var animatorComponent = ref entityPlayer.Get<AnimatorComponent>();

		GameObject player = Object.Instantiate(staticData.playerPrefab, sceneData.playerSpawnPoint.position, Quaternion.identity);
		runtimeData.TargetPositionForCamera = player.transform;

		player.transform.forward = staticData.GlobalForwardVector;

		animatorComponent.animator = player.GetComponent<Animator>();

		characterComponent.characterController = player.GetComponent<CharacterController>();
		movableComponent.transform = player.transform;
		movableComponent.speedMove = staticData.playerSpeed;
	}
}
