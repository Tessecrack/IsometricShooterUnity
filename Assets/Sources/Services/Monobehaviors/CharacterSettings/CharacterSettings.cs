using UnityEngine;

public class CharacterSettings : MonoBehaviour
{
	[Header("Motion")]
	[SerializeField] private float runSpeed = 6.0f;
	[SerializeField] private bool isMovable = true;

	[Header("Dash")]
	[SerializeField] private float dashSpeed = 80.0f;
	[SerializeField] private float dashTime = 0.06f;

	[Header("Health")]
	[SerializeField] private int maxHealth = 100;

	[Header("Count animations melee attack")]
	[SerializeField] private int countAnimationsMeleeAttack = 4;

	[Header("Count animations range attack")]
	[SerializeField] private int countAnimationsRangeAttack = 0;

	[Header("Speed motion in melee attack")]
	[SerializeField] private int speedMotionMeleeAttack = 40;

	public float RunSpeed => runSpeed;
	public int MaxHealth => maxHealth;
	public bool IsMovable => isMovable;
	public float DashSpeed => dashSpeed;
	public float DashTime => dashTime;
	public int CountAnimationsMeleeAttack => countAnimationsMeleeAttack;
	public int CountAnimationsRangeAttack => countAnimationsMeleeAttack;
	public int SpeedMotionMeleeAttack => speedMotionMeleeAttack;
}
