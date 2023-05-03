using Leopotam.Ecs;
using UnityEngine;

public class GameStartup : MonoBehaviour
{
    [Header("Objects/data in game")]
    public StaticData StaticData;

    [Header("Obejcts/data in current scene")]
    public SceneData SceneData;

    private Raycaster raycaster;
    private RuntimeData runtimeData;

    private EcsWorld ecsWorld;

    private EcsSystems ecsUpdateCharacterSystems;
    private EcsSystems ecsUpdateCameraSystems;
    
    private void Start()
    {
        SceneData.SetCamera();

        raycaster = new Raycaster();
        raycaster.SetCamera(SceneData.camera);
        raycaster.SetGroundLayer(StaticData.GetFloorLayer());

        runtimeData = new RuntimeData();

        ecsWorld = new EcsWorld();
		InitPlayerSystem();
		InitCameraSystem();
	}

    private void Update()
    {
        runtimeData.SetCursorPosition(raycaster.GetCursorPosition());
        ecsUpdateCharacterSystems?.Run();
		ecsUpdateCameraSystems?.Run();
	}

	private void OnDestroy()
	{
        ecsUpdateCharacterSystems?.Destroy();
        ecsUpdateCharacterSystems = null;

        ecsUpdateCameraSystems?.Destroy();
        ecsUpdateCameraSystems = null;

        ecsWorld?.Destroy();
        ecsWorld = null;
	}

    private void InitPlayerSystem()
    {
		ecsUpdateCharacterSystems = new EcsSystems(ecsWorld, "UPDATE SYSTEM CHARACTER");

		ecsUpdateCharacterSystems
			.Add(new PlayerInitSystem())
			.Add(new InputEventSystem())
			.Add(new CharacterDashTimerSystem())
			.Add(new PlayerMoveSystem())
			.Add(new PlayerSelectWeaponSystem())
			.Add(new PlayerAttackSystem())
			.Add(new CharacterChangeStateSystem())
			.Add(new CharacterAnimationSystem())
			.Add(new AttackSystem())
			.Inject(StaticData)
			.Inject(SceneData)
			.Inject(runtimeData);

		ecsUpdateCharacterSystems.Init();
	}

    private void InitCameraSystem()
    {
		ecsUpdateCameraSystems = new EcsSystems(ecsWorld, "UPDATE SYSTEM CAMERA");
		ecsUpdateCameraSystems
			.Add(new CameraInitSystem())
			.Add(new CameraFollowSystem())
			.Inject(StaticData)
			.Inject(SceneData)
			.Inject(runtimeData);

		ecsUpdateCameraSystems.Init();
	}
}
