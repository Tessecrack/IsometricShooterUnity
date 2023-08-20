using UnityEngine;

public class SingleShooter : Shooter
{
	public override void Shot(Vector3 targetPosition)
	{
		foreach (var pointSpawn in pointsSpawnShots)
		{
			OneShot(pointSpawn, targetPosition);
		}
	}
}
