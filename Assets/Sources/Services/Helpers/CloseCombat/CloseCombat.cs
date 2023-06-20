public class CloseCombat
{
	private int totalNumberStrikes;

	private int currentNumberStrike;

	public int TotalNumberStrikes => totalNumberStrikes;
	public int CurrentNumberStrike => currentNumberStrike;

	public CloseCombat(int totalNumberStrikes)
	{
		this.totalNumberStrikes = totalNumberStrikes;
	}

	public int GetNextStrike()
	{
		if (currentNumberStrike >= totalNumberStrikes)
		{
			currentNumberStrike = 0;
		}
		return currentNumberStrike++;
	}
}
