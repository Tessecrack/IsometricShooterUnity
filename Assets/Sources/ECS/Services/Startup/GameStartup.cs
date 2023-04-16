using Leopotam.Ecs;
using UnityEngine;

public class GameStartup : MonoBehaviour
{
    public StaticData StaticData;
    public SceneData SceneData;

    private EcsWorld ecsWorld;
    private EcsSystems ecsUpdateSystems;
    private EcsSystems ecsFixedUpdateSystems;
    
    private void Start()
    {
        SceneData.SetCamera();

        var runtimeData = new RuntimeData();

        ecsWorld = new EcsWorld();
        ecsUpdateSystems = new EcsSystems(ecsWorld, "UPDATE SYSTEM");
        ecsFixedUpdateSystems = new EcsSystems(ecsWorld, "FIXED UPDATE SYSTEM");

        ecsUpdateSystems
            .Add(new PlayerInitSystem())
            .Add(new CameraInitSystem())
            .Add(new InputMovementSystem())
            .Add(new CharacterMoveAnimationSystem())
            .Add(new CameraFollowSystem())
            .Inject(StaticData)
            .Inject(SceneData)
            .Inject(runtimeData);

        ecsFixedUpdateSystems
            .Add(new PlayerMoveSystem())
			.Inject(StaticData)
			.Inject(SceneData)
			.Inject(runtimeData);

		ecsUpdateSystems.Init();
        ecsFixedUpdateSystems.Init();
    }

    private void Update()
    {
        ecsUpdateSystems?.Run();
    }

	private void FixedUpdate()
	{
		ecsFixedUpdateSystems?.Run();
	}

	private void OnDestroy()
	{
        ecsUpdateSystems?.Destroy();
        ecsUpdateSystems = null;

        ecsFixedUpdateSystems?.Destroy();
        ecsFixedUpdateSystems = null;

        ecsWorld?.Destroy();
        ecsWorld = null;
	}
}
