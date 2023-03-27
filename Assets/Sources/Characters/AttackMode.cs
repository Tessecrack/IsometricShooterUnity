namespace Assets.Sources.Characters
{
	public class AttackMode
	{
		private readonly float timeAttackMode = 2.0f;
		private float currentTimeAttackMode = 0.0f;
		public bool IsActiveAttackMode { get; private set; }

		public void StartAttackMode(bool isMeleeWeapon)
		{
			if (IsActiveAttackMode && isMeleeWeapon)
			{
				return;
			}
			IsActiveAttackMode = true;
			currentTimeAttackMode = 0.0f;
		}

		public void IncreaseCurrentTimeAttackMode(float passedTime)
		{
			if (IsActiveAttackMode)
			{
				currentTimeAttackMode += passedTime;
				if (currentTimeAttackMode >= timeAttackMode)
				{
					StopAttackMode();
				}
			}
		}

		public void StopAttackMode()
		{
			IsActiveAttackMode = false;
			currentTimeAttackMode = 0;
		}
	}
}
