using Leopotam.EcsLite;
using UnityEngine;

public class GameStartup : MonoBehaviour
{
	private UserInput userInput;

    [Header("Objects/data in game")]
    public StaticData StaticData;

    [Header("Obejcts/data in current scene")]
    public SceneData SceneData;

    private Raycaster raycaster;
    private RuntimeData runtimeData;

	private SharedData sharedData = new();

    private EcsWorld ecsWorld;

	private EcsSystems ecsUpdateInputSystems;
	private EcsSystems ecsUpdatePlayerSystems;
	private EcsSystems ecsUpdateEnemySystems;
	private EcsSystems ecsUpdateCharacterSystems;
    private EcsSystems ecsUpdateCameraSystems;
	private EcsSystems ecsAttackSystems;
	private EcsSystems ecsDamageSystems;
	private EcsSystems ecsEnablerSystems;
	private EcsSystems ecsAnimationSystems;

	private EcsSystems ecsFixedUpdateSystems;

    private void Start()
    {
		SceneData.Init();

		raycaster = new Raycaster();
        raycaster.SetCamera(SceneData.Camera);
        raycaster.SetGroundLayer(StaticData.GetFloorLayer());
        runtimeData = new RuntimeData();
        ecsWorld = new EcsWorld();

		InitSharedData();
		InitPlayerSystem();
		InitEnemySystem();
		InitInputSystem();
		InitCharacterSystem();
		InitAttacksSystems();
		InitDamageSystem();
		InitCameraSystem();
		InitEnablerSystem();
		InitAnimationSystem();

		InitFixedUpdateSystems();
	}

	private void OnEnable() // TODO: NEED TO IMPROVE
	{
		this.userInput = UserInput.CreateUserInput();
		userInput.Enable();
	}

	private void OnDisable()
	{
		userInput.Disable();
	}

	private void Update()
	{
		runtimeData.SetCursorPosition(raycaster.GetCursorPosition());

		ecsUpdateInputSystems?.Run();

		ecsUpdatePlayerSystems?.Run();

		ecsUpdateEnemySystems?.Run();

		ecsAttackSystems?.Run();

		ecsUpdateCharacterSystems?.Run();

		ecsDamageSystems?.Run();

		ecsUpdateCameraSystems?.Run();

		ecsAnimationSystems?.Run();

		ecsEnablerSystems?.Run();
	}

	private void FixedUpdate()
	{
		ecsFixedUpdateSystems?.Run();
	}

	private void OnDestroy()
	{
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

		ecsEnablerSystems?.Destroy();
		ecsEnablerSystems = null;

		ecsAttackSystems?.Destroy();
		ecsAttackSystems = null;

		ecsDamageSystems?.Destroy();
		ecsDamageSystems = null;

		ecsAnimationSystems?.Destroy();
		ecsAnimationSystems = null;

		ecsFixedUpdateSystems?.Destroy();
		ecsFixedUpdateSystems = null;

        ecsWorld?.Destroy();
        ecsWorld = null;
	}

	private void InitSharedData()
	{
		sharedData.Init(StaticData, SceneData, runtimeData, userInput);
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
			.Add(new PlayerInputSystem())
			.Add(new PlayerDetectTargetSystem())
			.Add(new PlayerRuntimeActionSystem());
		ecsUpdatePlayerSystems.Init();
	}

	private void InitEnemySystem()
	{
		ecsUpdateEnemySystems = new EcsSystems(ecsWorld, sharedData);
		ecsUpdateEnemySystems
			.Add(new EnemyInitSystem())
			.Add(new EnemyDetectTargetSystem())
			.Add(new AIMotionSystem());
		ecsUpdateEnemySystems.Init();
	}

	private void InitFixedUpdateSystems()
	{
		ecsFixedUpdateSystems = new EcsSystems(ecsWorld, sharedData);	
		ecsFixedUpdateSystems.Init();
	}

	private void InitCharacterSystem()
	{
		ecsUpdateCharacterSystems = new EcsSystems(ecsWorld, sharedData);
		ecsUpdateCharacterSystems
			.Add(new CharacterAligmentWeaponSystem())
			.Add(new CharacterDashTimerSystem())
			.Add(new CharacterRotationSystem())
			.Add(new CharacterSelectWeaponSystem())
			.Add(new CharacterInputAttackSystem())
			.Add(new CharacterChangeStateSystem())
			.Add(new CharacterAimingSystem())
			.Add(new BaseAttackSystem())
			.Add(new CharacterRigSystem())
			.Add(new CharacterMoveSystem());
		ecsUpdateCharacterSystems.Init();
	}

	private void InitAnimationSystem()
	{
		ecsAnimationSystems = new EcsSystems(ecsWorld);
		ecsAnimationSystems.Add(new CharacterAnimationSystem());
		ecsAnimationSystems.Init();
	}

	private void InitAttacksSystems()
	{
		ecsAttackSystems = new EcsSystems(ecsWorld,sharedData);
		ecsAttackSystems
			.Add(new BaseAttackSystem())
			.Add(new BaseAttackMoveSystem());
		ecsAttackSystems.Init();
	}

	private void InitDamageSystem()
	{
		ecsDamageSystems = new EcsSystems(ecsWorld, sharedData);
		ecsDamageSystems
			.Add(new MeleeAttackPlayerDamageSystem())
			.Add(new MeleeAttackEnemyDamageSystem())
			.Add(new DamageHitSystem())
			.Add(new DamageSystem());
		ecsDamageSystems.Init();
	}

	private void InitCameraSystem()
    {
		ecsUpdateCameraSystems = new EcsSystems(ecsWorld, sharedData);
		ecsUpdateCameraSystems
			.Add(new CameraSystem());

		ecsUpdateCameraSystems.Init();
	}

	private void InitEnablerSystem()
	{
		ecsEnablerSystems = new EcsSystems(ecsWorld, sharedData);
		ecsEnablerSystems 
			.Add(new EnablerSystem());

		ecsEnablerSystems.Init();
	}
}
