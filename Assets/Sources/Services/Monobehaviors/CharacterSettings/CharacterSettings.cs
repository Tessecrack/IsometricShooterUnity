using UnityEngine;

public class CharacterSettings : MonoBehaviour
{
	[Header("Motion")]
	[Range(0.0f, 100.0f)]
	[SerializeField] private float runSpeed = 6.0f;
	[SerializeField] private bool isMovable = true;

	[Header("Dash")]
	[Range(0.0f, 100.0f)]
	[SerializeField] private float dashSpeed = 80.0f;
	[Range(0.0f, 1.0f)]
	[SerializeField] private float dashTime = 0.06f;

	[Header("Health")]
	[Range(0.0f, 100.0f)]
	[SerializeField] private int maxHealth = 100;

	[Header("Count animations melee attack")]
	[Range(0.0f, 100.0f)]
	[SerializeField] private int countAnimationsMeleeAttack = 4;

	[Header("Count animations range attack")]
	[Range(0.0f, 100.0f)]
	[SerializeField] private int countAnimationsRangeAttack = 0;

	[Header("Speed motion in melee attack")]
	[Range(0.0f, 100.0f)]
	[SerializeField] private int speedMotionMeleeAttack = 40;

	[Header("Count Death Animations")]
	[Range(0.0f, 100.0f)]
	[SerializeField] private int countDeathAnimations = 1;

	public float RunSpeed => runSpeed;
	public int MaxHealth => maxHealth;
	public bool IsMovable => isMovable;
	public float DashSpeed => dashSpeed;
	public float DashTime => dashTime;
	public int CountAnimationsMeleeAttack => countAnimationsMeleeAttack;
	public int CountAnimationsRangeAttack => countAnimationsRangeAttack;
	public int SpeedMotionMeleeAttack => speedMotionMeleeAttack;
	public int CountDeathAnimations => countDeathAnimations;
}
