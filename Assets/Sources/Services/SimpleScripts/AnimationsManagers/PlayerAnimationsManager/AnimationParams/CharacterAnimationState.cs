public class CharacterAnimationState
{
	public TypeWeapon CurrentTypeWeapon { get; set; }
	public bool IsMoving { get; set; }
	public bool IsAttackState { get; set; }
	public float HorizontalMoveValue { get; set; }
	public float VerticalMoveValue { get; set; }

	public void UpdateValuesState(CharacterAnimationState other)
	{
		CurrentTypeWeapon = other.CurrentTypeWeapon;
		IsMoving = other.IsMoving;
		IsAttackState = other.IsAttackState;
		HorizontalMoveValue = other.HorizontalMoveValue;
		VerticalMoveValue = other.VerticalMoveValue;
	}

	public bool Equals(CharacterAnimationState other)
	{
		return CurrentTypeWeapon == other.CurrentTypeWeapon &&
			IsMoving == other.IsMoving &&
			IsAttackState == other.IsAttackState &&
			HorizontalMoveValue == other.HorizontalMoveValue &&
			VerticalMoveValue == other.VerticalMoveValue;
	}
}
