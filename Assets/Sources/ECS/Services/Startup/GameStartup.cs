using Leopotam.Ecs;
using UnityEngine;

public class GameStartup : MonoBehaviour
{
    [Header("Objects/data in game")]
    public StaticData StaticData;

    [Header("Obejcts/data in current scene")]
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
            .Add(new InputEventSystem())
			.Add(new PlayerMoveSystem())
            .Add(new PlayerSelectWeaponSystem())
			.Add(new PlayerAttackSystem())
			.Add(new CharacterChangeStateSystem())
			.Add(new CharacterAnimationSystem())
            .Add(new CameraFollowSystem())
            .Add(new AttackSystem())
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
