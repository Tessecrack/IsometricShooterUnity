using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private CharacterController characterController;
    private ActorController owner;

    private Vector3 velocity;
    private float speed;
    private float damage;

    protected int ignoreCollisionBulletLayer = 3;
    protected int timeOfLife = 2;
	public void StartFire(ActorController owner, Vector3 target, float speed, float damage)
    {
		this.gameObject.layer = ignoreCollisionBulletLayer;
		characterController = GetComponent<CharacterController>();
		Destroy(this.gameObject, timeOfLife);

		this.owner = owner;
        this.speed = speed;
        this.damage = damage;
        this.velocity = Vector3.ClampMagnitude(target - owner.transform.position, 1);
        if (Vector3.Magnitude(this.velocity) < 1)
        {
            this.velocity = owner.GetForwardVector();
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
            Debug.Log(this.transform.position);
            characterController.Move(velocity * speed * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
    }
}
