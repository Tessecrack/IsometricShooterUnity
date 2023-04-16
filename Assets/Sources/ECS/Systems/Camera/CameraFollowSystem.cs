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

			cameraComponent.camera.transform.position = runtimeData.OwnerCameraTransform.position 
				+ cameraComponent.offset;

			runtimeData.CursorPosition = GetCursorPosition(cameraComponent.camera, runtimeData.OwnerCameraTransform.position);
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
