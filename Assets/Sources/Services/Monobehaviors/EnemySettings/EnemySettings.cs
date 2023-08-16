using UnityEngine;

public class EnemySettings : MonoBehaviour
{
	[SerializeField] private bool hasMeleeAttack;
	[SerializeField] private bool hasRangeAttack;
	[SerializeField] private int meleeDamage = 25;
	[SerializeField] private int rangeDamage = 25;
	[SerializeField] private float rangeDetectTarget = 5.0f;
	[SerializeField] private float distanceMeleeAttack = 1.5f;
	[SerializeField] private float distanceRangeAttack = 10.0f;

	public bool HasMeleeAttack => hasMeleeAttack;
	public bool HasRangeAttack => hasRangeAttack;
	public int MeleeDamage => meleeDamage;
	public int RangeDamage => rangeDamage;
	public float RangeDetectTarget => rangeDetectTarget;
	public float DistanceMeleeAttack => distanceMeleeAttack;
	public float DistanceRangeAttack => distanceRangeAttack;
}
