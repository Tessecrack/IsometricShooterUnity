using Leopotam.Ecs;
using UnityEngine;

public class CameraFollowSystem : IEcsRunSystem
{
	private EcsFilter<CameraComponent> filter;
	private StaticData staticData;
	private RuntimeData runtimeData;
	public void Run()
	{
		foreach(var i in filter)
		{
			ref var cameraComponent = ref filter.Get1(i);

			var pointCameraOwner = runtimeData.OwnerCameraTransform.position;
			var cursorPosition = GetCursorPosition(cameraComponent.camera, pointCameraOwner);
			runtimeData.CursorPosition = cursorPosition;


			var currentAngle = Vector3.Angle(staticData.GlobalForwardVector, cursorPosition - pointCameraOwner);

			var coeffInterpolate = currentAngle > 90 ? 3.0f : 2.0f;

			var additionOffsetDown = MapValue(currentAngle, 90, 180, 0, 2);

			var result = pointCameraOwner + cameraComponent.offset;
			var distance = Vector3.ClampMagnitude(cursorPosition - result, staticData.CameraMagnitude);

			var currentPosition = cameraComponent.camera.transform.position;
			var targetPosition = new Vector3(result.x + distance.x + additionOffsetDown,
				pointCameraOwner.y + cameraComponent.offset.y,
				result.z + distance.z - additionOffsetDown);

			cameraComponent.camera.transform.position = Vector3.Slerp(currentPosition, targetPosition,
				coeffInterpolate * Time.deltaTime);
		}
	}

	private Vector3 GetCursorPosition(Camera camera, Vector3 ownerPosition)
	{
		Ray rayFromCursor = camera.ScreenPointToRay(Input.mousePosition);
		RaycastHit raycastHit;
		int layerMask = 1 << staticData.GetFloorLayer();
		Physics.Raycast(rayFromCursor, out raycastHit, int.MaxValue, layerMask);
		return new Vector3(raycastHit.point.x, ownerPosition.y, raycastHit.point.z);
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
