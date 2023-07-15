using UnityEngine;

public class EnemySettings : MonoBehaviour
{
	[SerializeField] private TypeEnemy typeEnemy;
	[SerializeField] private int meleeDamage = 25;
	[SerializeField] private int rangeDamage = 25;
	[SerializeField] private float rangeDetectTarget = 5.0f;
	[SerializeField] private float rangeMeleeAttack = 1.5f;
	[SerializeField] private float distanceRangeAttack = 10.0f;

	public TypeEnemy TypeEnemy => typeEnemy;
	public int MeleeDamage => meleeDamage;
	public int RangeDamage => rangeDamage;
	public float RangeDetectTarget => rangeDetectTarget;
	public float RangeMeleeAttack => rangeMeleeAttack;
	public float DistanceRangeAttack => distanceRangeAttack;
}
