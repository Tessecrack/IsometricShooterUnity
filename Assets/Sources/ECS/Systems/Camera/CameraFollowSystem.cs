using Leopotam.Ecs;

public class CameraFollowSystem : IEcsRunSystem
{
	private EcsFilter<CameraComponent> filter;
	private RuntimeData runtimeData;
	public void Run()
	{
		foreach(var i in filter)
		{
			ref var cameraComponent = ref filter.Get1(i);

			cameraComponent.camera.transform.position = runtimeData.TargetPositionForCamera.position 
				+ cameraComponent.offset;
		}
	}
}
