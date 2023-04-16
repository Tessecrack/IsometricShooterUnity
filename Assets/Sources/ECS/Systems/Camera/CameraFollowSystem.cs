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

			var cursorPosition = GetCursorPosition(cameraComponent.camera, runtimeData.OwnerCameraTransform.position);

			runtimeData.CursorPosition = cursorPosition;

			var result = pointCameraOwner + cameraComponent.offset;

			var distance = Vector3.ClampMagnitude(cursorPosition - pointCameraOwner, 3.0f);

			Debug.DrawLine(pointCameraOwner, distance + pointCameraOwner);

			cameraComponent.camera.transform.position = new Vector3(result.x, pointCameraOwner.y + cameraComponent.offset.y, result.z);
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
}
