using Leopotam.EcsLite;
using UnityEngine;

public class CameraInitSystem : IEcsInitSystem
{
	public void Init(IEcsSystems systems)
	{
		EcsWorld world = systems.GetWorld();
		int entityCamera = world.NewEntity();

		var sharedData = systems.GetShared<SharedData>();

		EcsPool<CameraComponent> pool = world.GetPool<CameraComponent>();

		ref var cameraComponent = ref pool.Add(entityCamera);

		cameraComponent.camera = sharedData.SceneData.Camera;
		cameraComponent.owner = sharedData.RuntimeData.OwnerCameraTransform;

		cameraComponent.offset = sharedData.StaticData.CameraSettings.CameraOffset;
		cameraComponent.camera.transform.rotation = Quaternion.Euler(sharedData.StaticData.CameraSettings.AngleCameraX,
			sharedData.StaticData.CameraSettings.AngleCameraY, 0.0f);
	}
}
