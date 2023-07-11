using System;
using UnityEngine;

public class CloseCombat : MonoBehaviour
{
	[SerializeField] private int totalNumberStrikes = 4;
	[SerializeField] private int speedMoveForwardInStrike = 30;
	[SerializeField] private float rangeAttack = 1.5f;

	private float passedTimeLastStrike = 0;
	private readonly float timeReset = 0.4f;

	public bool AttackInProccess { get; private set; }
	public bool IsStartAttack { get; private set; }
	public bool IsEndAttack { get; private set; }
	public int CurrentNumberStrike { get; private set; }
	public bool IsApplyDamage { get; private set; }
	public bool NeedForwardMove { get; private set; }

	public event Action EventStartAttack;

	public event Action EventEndAttack;

	public event Action EventStartApplyDamage;

	public event Action EventEndApplyDamage;

	public int TotalNumberStrikes => totalNumberStrikes;
	public int SpeedMoveForwardInStrike => speedMoveForwardInStrike;
	public int GetNextStrike()
	{
		if (CurrentNumberStrike >= totalNumberStrikes)
		{
			CurrentNumberStrike = 0;
		}
		return CurrentNumberStrike++;
	}

	public int GetNextRandomStrike()
	{
		var randomNumber = UnityEngine.Random.Range(0, totalNumberStrikes);
		return randomNumber;
	}

	public void SetNeedStrike()
	{
		IsStartAttack = true;
	}

	private void Update()
	{
		if (IsEndAttack)
		{
			passedTimeLastStrike += Time.deltaTime;
		}
		if (IsStartAttack)
		{
			passedTimeLastStrike = 0;
		}
		if (passedTimeLastStrike >= timeReset)
		{
			passedTimeLastStrike = 0;
			CurrentNumberStrike = 0;
		}
	}

	/*Handlers events from animator*/
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
