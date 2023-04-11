public class TurretController : ActorController
{
	private AIController agent;
	private FastTurretWeapon turretWeapon;

	protected override void InitController()
	{
		attackMode = new AttackMode();
		actorMovement = new ActorMovement();
		health = new ActorHealth();

		turretWeapon = GetComponent<FastTurretWeapon>();

		var player = FindObjectOfType<PlayerController>();
		agent = AIController.InitAIController(this.transform, player.transform, player.gameObject.layer);

		OnStartAttack += StartAttack;
		OnStopAttack += StopAttack;
		OnTakeDamage += health.TakeDamage;
	}

	protected override void UpdateAttackMode()
	{
		if (agent.CanAttack)
		{
			OnStartAttack?.Invoke();
		}
		else
		{
			OnStopAttack?.Invoke();
		}
	}

	protected override void UpdateTargetPoint()
	{
		actorMovement.UpdateTargetPoint(agent.GetTargetPosition());
	}
	public override Weapon GetCurrentWeapon() => turretWeapon;
	protected override void UpdateMovementActor()
	{
		
	}

	protected override void UpdateWeapon()
	{
		
	}

	protected override void SetDefaultWeapon()
	{

	}
}
