using UnityEngine;

public class SpreadShooter : Shooter
{
	public override void Shot(Vector3 targetPosition)
	{
		var partBullets = quantityOneShotProjectile / 2;
		foreach (var pointSpawnShot in pointsSpawnShots)
		{
			for (int i = -partBullets; i <= partBullets; ++i)
			{
				OneShot(pointSpawnShot, targetPosition + i * pointSpawnShot.right.normalized);
			}
		}
	}
}
