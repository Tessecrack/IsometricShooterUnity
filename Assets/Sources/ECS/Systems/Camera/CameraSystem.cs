using Leopotam.EcsLite;
using UnityEngine;

public class CameraSystem : IEcsInitSystem, IEcsRunSystem
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

	public void Run(IEcsSystems systems)
	{
		var world = systems.GetWorld();
		var filter = world.Filter<CameraComponent>().End();
		var cameras = world.GetPool<CameraComponent>();

		var sharedData = systems.GetShared<SharedData>();
		var staticData = sharedData.StaticData;
		var runtimeData = sharedData.RuntimeData;

		var pointCameraOwner = runtimeData.OwnerCameraTransform;
		var cursorPosition = runtimeData.GetModifyCursorPosition();

		foreach (int entity in filter)
		{
			ref var cameraComponent = ref cameras.Get(entity);

			var currentAngle = Vector3.Angle(staticData.GlobalForwardVector, cursorPosition - pointCameraOwner);

			var cameraPosition = cameraComponent.camera.transform.position;

			var cameraRelativePlayer = new Vector3(cameraPosition.x, pointCameraOwner.y, cameraPosition.z);

			var distancePlayerToCamera = Vector3.Distance(cameraRelativePlayer, pointCameraOwner);

			var needOffset = distancePlayerToCamera <= 2;

			var coeffInterpolate = currentAngle > 90 || needOffset ? 4.0f : 3.0f;
			var additionOffsetDown = MapValue(currentAngle, 90, 180, 0, 2);

			var result = pointCameraOwner + cameraComponent.offset;
			var distance = Vector3.ClampMagnitude(cursorPosition - result, staticData.CameraSettings.CameraMagnitude);

			var currentPosition = cameraComponent.camera.transform.position;
			var targetPosition = new Vector3(result.x + distance.x + additionOffsetDown,
				pointCameraOwner.y + cameraComponent.offset.y,
				result.z + distance.z - additionOffsetDown);

			cameraComponent.camera.transform.position = Vector3.Slerp(currentPosition, targetPosition,
				coeffInterpolate * Time.deltaTime);
		}
	}

	private float MapValue(float value, float leftMin, float leftMax, float rightMin, float rightMax)
	{
		if (value < leftMin)
		{
			return 0.0f;
		}

		var leftSpan = leftMax - leftMin;
		var rightSpan = rightMax - rightMin;
		var valueScaled = (float)(value - leftMin) / (float)(leftSpan);
		return rightMin + (valueScaled * rightSpan);
	}
}
