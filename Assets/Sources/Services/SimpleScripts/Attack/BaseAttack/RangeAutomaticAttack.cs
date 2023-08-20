public class RangeAutomaticAttack : RangeAttack
{
	public RangeAutomaticAttack(Shooter shooter) : base(shooter)
	{
	}

	public override void StartAttack()
	{
		if (canAttack)
		{
			base.StartAttack();
			canAttack = false;
		}
	}

	public override void EndAttack()
	{
		base.EndAttack();
		canAttack = true;
	}
}
