using Leopotam.Ecs;
using UnityEngine;

public class CameraInitSystem : IEcsInitSystem
{
	private EcsWorld ecsWorld;

	private StaticData staticData;
	private SceneData sceneData;
	private RuntimeData runtimeData;

	public void Init()
	{
		var entityCamera = ecsWorld.NewEntity();

		ref var cameraComponent = ref entityCamera.Get<CameraComponent>();

		cameraComponent.camera = sceneData.camera;
		cameraComponent.owner = runtimeData.OwnerCameraTransform;

		cameraComponent.offset = staticData.CameraOffset;
		cameraComponent.camera.transform.rotation = Quaternion.Euler(staticData.GetAngleCameraX(), staticData.GetAngleCameraY(), 0.0f);
	}
}
