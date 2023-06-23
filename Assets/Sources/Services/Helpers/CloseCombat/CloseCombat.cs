using System;
using UnityEngine;

public class CloseCombat : MonoBehaviour
{
	private int totalNumberStrikes;

	private int currentNumberStrike;

	public bool IsAttacking { get; private set; }
	public bool NeedStrike { get; private set; }
	public bool IsEndAttack { get; private set; }
	public int TotalNumberStrikes => totalNumberStrikes;
	public int CurrentNumberStrike => currentNumberStrike;

	public event Action EventStartAttack;

	public event Action EventEndAttack;

	public event Action EventStartApplyDamage;

	public event Action EventEndApplyDamage;


	public int GetNextStrike()
	{
		if (currentNumberStrike >= totalNumberStrikes)
		{
			currentNumberStrike = 0;
		}
		return currentNumberStrike++;
	}

	public void SetNeedStrike()
	{
		NeedStrike = true;
	}

	public void SetTotalNumbersStrikes(int totalNumberStrikes)
	{
		this.totalNumberStrikes = totalNumberStrikes;
	}

	/*Handlers events from animators*/
	public void HandlerStartAttack()
	{
		if (IsAttacking)
		{
			return;
		}
		EventStartAttack?.Invoke();
		IsEndAttack = false;
	}

	public void HandlerEndAttack()
	{
		EventEndAttack?.Invoke();
		NeedStrike = false;
		IsAttacking = false;
		IsEndAttack = true;
	}

	public void HandlerStartApplyDamage()
	{
		EventStartApplyDamage?.Invoke();
	}

	public void HandlerEndApplyDamage()
	{
		EventEndApplyDamage?.Invoke();
	}	
	/*----------------*/
}
