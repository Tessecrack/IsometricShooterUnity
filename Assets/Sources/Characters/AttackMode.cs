using UnityEngine;

namespace Assets.Sources.Characters
{
	public class AttackMode
	{
		private ActorController actor;

		private readonly float timeAttackModeRangedWeapon = 2.0f;

		private float timeAttackMode;

		private float currentTimeAttackMode = 0.0f;

		public bool IsStartAttack { get; private set; }

		public bool IsStopAttack { get; private set; }

		public bool IsActiveAttackMode { get; private set; }
		public AttackMode(ActorController actor)
		{
			this.actor = actor;
		}

		public void SetStartAttack()
		{
			IsStartAttack = true;
		}

		public void SetStopAttack()
		{
			IsStopAttack = true;
		}

		public void Reset()
		{
			IsStartAttack = false;
			IsStopAttack = false;
		}

		public void IncreaseCurrentTimeAttackMode(float passedTime)
		{
			if (IsActiveAttackMode)
			{
				currentTimeAttackMode += passedTime;
				if (currentTimeAttackMode >= timeAttackMode)
				{
					DeactivateAttackMode();
				}
			}
		}

		public void DeactivateAttackMode()
		{
			IsActiveAttackMode = false;
			currentTimeAttackMode = 0;
		}

		public void StartAttack(Vector3 target)
		{
			var currentWeapon = actor.GetCurrentWeapon();
			var currentTypeWeapon = currentWeapon.CurrentTypeWeapon;
			switch(currentTypeWeapon)
			{
				case TypeWeapon.MELEE:
					timeAttackMode = currentWeapon.DelayBetweenAttack;
					break;
				default:
					timeAttackMode = timeAttackModeRangedWeapon;
					break;
			}
			StartAttackMode(currentWeapon.CurrentTypeWeapon == TypeWeapon.MELEE);
			currentWeapon.StartAttack(actor, target);
		}

		public void StopAttack()
		{
			actor.GetCurrentWeapon().StopAttack();
		}

		private void StartAttackMode(bool isMeleeWeapon)
		{
			if (IsActiveAttackMode && isMeleeWeapon)
			{
				return;
			}
			IsActiveAttackMode = true;
			currentTimeAttackMode = 0.0f;
		}
	}
}
