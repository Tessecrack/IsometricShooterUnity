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
        ecsWorld = new EcsWorld();
        ecsUpdateSystems = new EcsSystems(ecsWorld, "UPDATE SYSTEM");
        ecsFixedUpdateSystems = new EcsSystems(ecsWorld, "FIXED UPDATE SYSTEM");

        ecsUpdateSystems
            .Add(new PlayerInitSystem())
            .Add(new InputMovementSystem())
            .Inject(StaticData)
            .Inject(SceneData);

        ecsFixedUpdateSystems
            .Add(new PlayerMoveSystem());

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
