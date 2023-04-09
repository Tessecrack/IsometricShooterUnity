using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 velocity;
    private float speed;
    private float damage;
    private int ignoreCollisionBulletLayer = 3;
    private int timeOfLife = 2;

	public void StartFire(TurretController owner, Vector3 target, float speed, float damage)
	{
        StartFire(owner.transform, target, speed, damage);
	}

	public void StartFire(ActorController owner, Vector3 target, float speed, float damage)
    {
		StartFire(owner.transform, target, speed, damage);
	}

    private void StartFire(Transform owner, Vector3 target, float speed, float damage)
    {
		this.gameObject.layer = ignoreCollisionBulletLayer;
		characterController = GetComponent<CharacterController>();
		Destroy(this.gameObject, timeOfLife);
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
        if (hit.gameObject.TryGetComponent<ActorController>(out ActorController actor))
        {
            actor.TakeDamage(damage);
        }
        Destroy(this.gameObject);
    }

    IEnumerator Fire()
    {
        while(true)
        {
            characterController.Move(velocity * speed * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
    }
}
