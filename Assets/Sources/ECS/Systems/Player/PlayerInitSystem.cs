using Leopotam.Ecs;
using UnityEngine;

public class PlayerInitSystem : IEcsInitSystem
{
	private EcsWorld ecsWorld;
	private StaticData staticData;
	private SceneData sceneData;

	public void Init()
	{
		var entityPlayer = ecsWorld.NewEntity();

		ref var characterComponent = ref entityPlayer.Get<CharacterComponent>();
		ref var inputComponent = ref entityPlayer.Get<InputMovementComponent>();
		ref var movableComponent = ref entityPlayer.Get<MovableComponent>();

		GameObject player = Object.Instantiate(staticData.playerPrefab, sceneData.playerSpawnPoint.position, Quaternion.identity);

		characterComponent.characterController = player.GetComponent<CharacterController>();
		movableComponent.transform = player.transform;
		movableComponent.speedMove = staticData.playerSpeed;
	}
}
