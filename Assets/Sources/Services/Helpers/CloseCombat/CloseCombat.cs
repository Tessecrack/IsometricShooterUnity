using UnityEngine;

public class CloseCombat : MonoBehaviour
{
	[SerializeField] private int totalNumberStrikes;

	private int currentNumberStrike;

	public int TotalNumberStrikes => totalNumberStrikes;
	public int CurrentNumberStrike => currentNumberStrike;

	public int GetNextStrike()
	{
		if (currentNumberStrike >= totalNumberStrikes)
		{
			currentNumberStrike = 0;
		}
		return currentNumberStrike++;
	}
}
