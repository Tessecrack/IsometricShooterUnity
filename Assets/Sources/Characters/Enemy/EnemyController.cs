public class EnemyController : ActorController
{
	private AIController agent;
	public TypeEnemy TypeEnemy { get; protected set; }

	private readonly int defaultNumberWeapon = 0;

	protected override void InitController()
	{
		base.InitController();
		var player = FindObjectOfType<PlayerController>();
		agent = AIController.InitAIController(this.transform, player.transform, player.gameObject.layer);
	}

	protected override void UpdateTargetPoint()
	{
		actorMovement.UpdateTargetPoint(agent.GetTargetPosition());
	}

	protected override void UpdateMovementActor()
	{
		
	}

	protected override void UpdateAttackMode()
	{
		if (agent.IsPlayerFounded)
		{
			attackMode.Enable();
		}
		else
		{
			attackMode.Disable();
		}
	}

	protected override void UpdateWeapon()
	{

	}

	protected override void SetDefaultWeapon()
	{
		arsenal.SetInitialWeapon(defaultNumberWeapon);
		CurrentTypeWeapon = arsenal.GetCurrentWeapon().CurrentTypeWeapon;
	}
}
