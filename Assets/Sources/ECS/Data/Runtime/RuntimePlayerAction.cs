using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimePlayerAction
{
	private int damageCloseCombat;

	public void SetDamageCloseCombat(int damageCloseCombat)
	{
		this.damageCloseCombat = damageCloseCombat;
	}

	public void HandlerPlayerCloseCombatStart()
	{
		IsPlayerCloseCombatAttack = true;
	}

	public void HandlerPlayerCloseCombatEnd()
	{
		IsPlayerCloseCombatAttack = false;
	}

	public int DamageCloseCombat => damageCloseCombat;

	public bool IsPlayerCloseCombatAttack { get; private set; }

	public bool IsPlayerDeath { get; private set; }
}
