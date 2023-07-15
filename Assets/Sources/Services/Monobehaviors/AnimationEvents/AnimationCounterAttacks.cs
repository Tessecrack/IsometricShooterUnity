public class AnimationCounterAttacks
{
	private readonly int countAnimationAttacks;
	private int currentNumberAnimationAttack = 0;

	public AnimationCounterAttacks(int countAnimationAttacks)
	{
		this.countAnimationAttacks = countAnimationAttacks;
	}

	public void Increase()
	{
		if (this.currentNumberAnimationAttack >= this.countAnimationAttacks - 1)
		{
			currentNumberAnimationAttack = 0;
			return;
		}
		++currentNumberAnimationAttack;
	}

	public void ResetCounter()
	{
		currentNumberAnimationAttack = 0;
	}
	public int CurrentNumberAnimation => currentNumberAnimationAttack;
	public int RandomNumberAnimation => UnityEngine.Random.Range(0, countAnimationAttacks);
	public int TotalNumberAttacks => countAnimationAttacks;

}
