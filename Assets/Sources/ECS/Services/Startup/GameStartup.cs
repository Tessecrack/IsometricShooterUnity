using Leopotam.EcsLite;
using UnityEngine;

public class GameStartup : MonoBehaviour
{
    [Header("Objects/data in game")]
    public StaticData StaticData;

    [Header("Obejcts/data in current scene")]
    public SceneData SceneData;

    private Raycaster raycaster;
    private RuntimeData runtimeData;

	private SharedData sharedData = new SharedData();

    private EcsWorld ecsWorld;

    private EcsSystems ecsUpdateCharacterSystems;
    private EcsSystems ecsUpdateCameraSystems;
    
    private void Start()
    {
        raycaster = new Raycaster();
        raycaster.SetCamera(SceneData.Camera);
        raycaster.SetGroundLayer(StaticData.GetFloorLayer());

        runtimeData = new RuntimeData();

        ecsWorld = new EcsWorld();

		InitSharedData();

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

	private void InitSharedData()
	{
		sharedData.InitSharedData(StaticData, SceneData, runtimeData);
	}

    private void InitPlayerSystem()
    {
		ecsUpdateCharacterSystems = new EcsSystems(ecsWorld, sharedData);
		ecsUpdateCharacterSystems
			.Add(new PlayerInitSystem())
			.Add(new InputEventSystem())
			.Add(new CharacterDashTimerSystem())
			.Add(new PlayerMoveSystem())
			.Add(new PlayerSelectWeaponSystem())
			.Add(new PlayerAttackSystem())
			.Add(new CharacterChangeStateSystem())
			.Add(new CharacterAnimationSystem())
			.Add(new AttackSystem());

		ecsUpdateCharacterSystems.Init();
	}

    private void InitCameraSystem()
    {
		ecsUpdateCameraSystems = new EcsSystems(ecsWorld, sharedData);
		ecsUpdateCameraSystems
			.Add(new CameraInitSystem())
			.Add(new CameraFollowSystem());

		ecsUpdateCameraSystems.Init();
	}
}
