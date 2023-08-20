public class RangeSimpleAttack : RangeAttack
{
	private bool needClickTrigger = false;

	public RangeSimpleAttack(Shooter shooter) : base(shooter)
	{
	}

	public override void StartAttack()
	{
		if (canAttack && !needClickTrigger)
		{
			base.StartAttack();
			canAttack = false;
			needClickTrigger = true;
		}
	}

	public override void EndAttack()
	{
		base.EndAttack();
		needClickTrigger = false;
	}
}
