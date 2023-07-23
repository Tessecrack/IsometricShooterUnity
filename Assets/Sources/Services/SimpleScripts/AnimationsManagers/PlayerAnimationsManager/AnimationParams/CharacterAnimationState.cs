public class CharacterAnimationState
{
	public TypeWeapon CurrentTypeWeapon { get; set; }
	public bool IsMoving { get; set; }
	public bool IsAimingState { get; set; }
	public float HorizontalMoveValue { get; set; }
	public float VerticalMoveValue { get; set; }
	public bool IsMeleeAttack { get; set; }

	public void UpdateValuesState(CharacterAnimationState other)
	{
		CurrentTypeWeapon = other.CurrentTypeWeapon;
		IsMoving = other.IsMoving;
		IsAimingState = other.IsAimingState;
		HorizontalMoveValue = other.HorizontalMoveValue;
		VerticalMoveValue = other.VerticalMoveValue;
		IsMeleeAttack = other.IsMeleeAttack;
	}

	public bool Equals(CharacterAnimationState other)
	{
		return CurrentTypeWeapon == other.CurrentTypeWeapon &&
			IsMoving == other.IsMoving &&
			IsAimingState == other.IsAimingState &&
			IsMeleeAttack == other.IsMeleeAttack;
	}

	public bool EqualsBlendTreeParams(CharacterAnimationState other)
	{
		return HorizontalMoveValue == other.HorizontalMoveValue &&
			VerticalMoveValue == other.VerticalMoveValue;
	}
}
