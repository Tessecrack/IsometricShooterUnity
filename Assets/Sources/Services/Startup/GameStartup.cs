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

	private EcsSystems ecsUpdateInputSystems;
	private EcsSystems ecsUpdatePlayerSystems;
	private EcsSystems ecsUpdateTurretSystems;
	private EcsSystems ecsUpdateEnemySystems;
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

		InitTurretSystem();
		InitEnemySystem();
		InitInputSystem();
		InitPlayerSystem();
		InitCharacterSystem();
		InitCameraSystem();
	}

    private void Update()
    {
        runtimeData.SetCursorPosition(raycaster.GetCursorPosition());

		ecsUpdateInputSystems?.Run();
		ecsUpdatePlayerSystems?.Run();
		ecsUpdateTurretSystems?.Run();
        ecsUpdateCharacterSystems?.Run();
		ecsUpdateCameraSystems?.Run();
	}

	private void OnDestroy()
	{
		ecsUpdateTurretSystems?.Destroy();
		ecsUpdateTurretSystems = null;

		ecsUpdateEnemySystems?.Destroy();
		ecsUpdateEnemySystems = null;

		ecsUpdateInputSystems?.Destroy();
		ecsUpdateInputSystems = null;

		ecsUpdatePlayerSystems?.Destroy();
		ecsUpdatePlayerSystems = null;

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

	private void InitInputSystem()
	{
		ecsUpdateInputSystems = new EcsSystems(ecsWorld, sharedData);
		ecsUpdateInputSystems
			.Add(new InputEventSystem());

		ecsUpdateInputSystems.Init();
	}

    private void InitPlayerSystem()
    {
		ecsUpdatePlayerSystems = new EcsSystems(ecsWorld, sharedData);
		ecsUpdatePlayerSystems
			.Add(new PlayerInitSystem())
			.Add(new PlayerInputSystem());

		ecsUpdatePlayerSystems.Init();
	}

	private void InitEnemySystem()
	{
		ecsUpdateEnemySystems = new EcsSystems(ecsWorld, sharedData);
		ecsUpdateEnemySystems
			.Add(new EnemyInitSystem());
		ecsUpdateEnemySystems.Init();
	}

	private void InitTurretSystem()
	{
		ecsUpdateTurretSystems = new EcsSystems(ecsWorld, sharedData);
		ecsUpdateTurretSystems
			.Add(new TurretInitSystem());

		ecsUpdateTurretSystems.Init();
	}

	private void InitCharacterSystem()
	{
		ecsUpdateCharacterSystems = new EcsSystems(ecsWorld, sharedData);
		ecsUpdateCharacterSystems
			.Add(new CharacterAimingSystem())
			.Add(new CharacterDashTimerSystem())
			.Add(new CharacterMoveSystem())
			.Add(new CharacterSelectWeaponSystem())
			.Add(new CharacterAttackSystem())
			.Add(new CharacterChangeStateSystem())
			.Add(new CharacterAnimationSystem())
			.Add(new AttackSystem())
			.Add(new CharacterRigSystem());

		ecsUpdateCharacterSystems.Init();
	}

    private void InitCameraSystem()
    {
		ecsUpdateCameraSystems = new EcsSystems(ecsWorld, sharedData);
		ecsUpdateCameraSystems
			.Add(new CameraSystem());

		ecsUpdateCameraSystems.Init();
	}
}