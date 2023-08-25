public class CharacterAnimationState
{
	public TypeWeapon CurrentTypeWeapon { get; set; }
	public TypeAttack TypeAttack { get; set; }
	public CharacterState CharacterState { get; set; }
	public AimState AimState { get; set; }
	public float HorizontalMoveValue { get; set; }
	public float VerticalMoveValue { get; set; }
	public bool IsAttack { get; set; }
	public void UpdateValuesState(CharacterAnimationState other)
	{
		CurrentTypeWeapon = other.CurrentTypeWeapon;
		CharacterState = other.CharacterState;
		TypeAttack = other.TypeAttack;
		AimState = other.AimState;
		HorizontalMoveValue = other.HorizontalMoveValue;
		VerticalMoveValue = other.VerticalMoveValue;
		IsAttack = other.IsAttack;
	}

	public bool Equals(CharacterAnimationState other)
	{
		return CurrentTypeWeapon == other.CurrentTypeWeapon &&
			CharacterState == other.CharacterState &&
			TypeAttack == other.TypeAttack &&
			AimState == other.AimState &&
			IsAttack == other.IsAttack;
	}

	public bool EqualsBlendTreeParams(CharacterAnimationState other)
	{
		return HorizontalMoveValue == other.HorizontalMoveValue &&
			VerticalMoveValue == other.VerticalMoveValue;
	}
}
