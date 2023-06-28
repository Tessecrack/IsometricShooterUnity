using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	private CharacterController characterController;
	private Vector3 velocity;
	private float speed;
	private int damage;
	private int ignoreCollisionBulletLayer = 3;
	private int lifeTime = 2;

	public void StartFire(Transform owner, Vector3 target, float speed, int damage)
	{
		this.gameObject.layer = ignoreCollisionBulletLayer;
		characterController = GetComponent<CharacterController>();
		Destroy(this.gameObject, lifeTime);
		this.speed = speed;
		this.damage = damage;
		this.velocity = Vector3.ClampMagnitude(target - owner.transform.position, 1);
		if (Vector3.Magnitude(this.velocity) < 1)
		{
			this.velocity = owner.forward;
		}
		StartCoroutine(Fire());
	}

	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (hit.gameObject.layer == ignoreCollisionBulletLayer)
		{
			return;
		}

		if (hit.gameObject.TryGetComponent<Damageable>(out Damageable damageableObj))
		{
			damageableObj.TakeDamage(damage);
		}

		Destroy(this.gameObject);
	}

	IEnumerator Fire()
	{
		while (true)
		{
			characterController.Move(velocity * speed * Time.fixedDeltaTime);
			yield return new WaitForFixedUpdate();
		}
	}
}
