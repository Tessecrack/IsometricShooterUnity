using Leopotam.Ecs;
using UnityEngine;

public class GameStartup : MonoBehaviour
{
    [Header("Scriptable objects")]
    public StaticData StaticData;

    [Header("Scene data")]
    public SceneData SceneData;

    private EcsWorld ecsWorld;
    private EcsSystems ecsUpdateSystems;
    
    private void Start()
    {
        SceneData.SetCamera();

        var runtimeData = new RuntimeData();

        ecsWorld = new EcsWorld();
        ecsUpdateSystems = new EcsSystems(ecsWorld, "UPDATE SYSTEM");

        ecsUpdateSystems
            .Add(new PlayerInitSystem())
            .Add(new CameraInitSystem())
            .Add(new InputMovementSystem())
			.Add(new PlayerMoveSystem())
			.Add(new CharacterMoveAnimationSystem())
            .Add(new CameraFollowSystem())
            .Inject(StaticData)
            .Inject(SceneData)
            .Inject(runtimeData);

		ecsUpdateSystems.Init();
    }

    private void Update()
    {
        ecsUpdateSystems?.Run();
    }

	private void OnDestroy()
	{
        ecsUpdateSystems?.Destroy();
        ecsUpdateSystems = null;

        ecsWorld?.Destroy();
        ecsWorld = null;
	}
}
