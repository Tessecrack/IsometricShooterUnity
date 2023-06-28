using System;
using UnityEngine;

public class CloseCombat : MonoBehaviour
{
	[SerializeField] private int totalNumberStrikes = 0;

	public bool AttackInProccess { get; private set; }
	public bool IsStartAttack { get; private set; }
	public bool IsEndAttack { get; private set; }
	public int CurrentNumberStrike { get; private set; }

	public bool IsApplyDamage { get; private set; }
	public bool NeedForwardMove { get; private set; }

	public float DistanceAttack => 1.5f;

	public event Action EventStartAttack;

	public event Action EventEndAttack;

	public event Action EventStartApplyDamage;

	public event Action EventEndApplyDamage;

	public int TotalNumberStrikes => totalNumberStrikes;

	public int GetNextStrike()
	{
		if (CurrentNumberStrike >= totalNumberStrikes)
		{
			CurrentNumberStrike = 0;
		}
		return CurrentNumberStrike++;
	}

	public void SetNeedStrike()
	{
		IsStartAttack = true;
	}

	/*Handlers events from animators*/
	public void HandlerStartAttack()
	{
		if (AttackInProccess)
		{
			return;
		}
		EventStartAttack?.Invoke();
		IsEndAttack = false;
	}

	public void HandlerEndAttack()
	{
		EventEndAttack?.Invoke();
		IsStartAttack = false;
		AttackInProccess = false;
		IsEndAttack = true;
	}

	public void HandlerStartApplyDamage()
	{
		IsApplyDamage = true;
		EventStartApplyDamage?.Invoke();
	}

	public void HandlerEndApplyDamage()
	{
		IsApplyDamage = false;
		EventEndApplyDamage?.Invoke();
	}	

	public void HandlerStartForwardMove()
	{
		NeedForwardMove = true;
	}

	public void HandlerEndForwardMove()
	{
		NeedForwardMove = false;
	}
	/*----------------*/
}
