using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private CharacterController characterController;
    private ActorController owner;

    private Vector3 velocity;
    private Vector3 target;
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
        this.target = target;
        this.speed = speed;
        this.damage = damage;

        this.velocity = Vector3.Normalize(target - this.transform.position);
        this.velocity.y = target.y;

        StartCoroutine(Fire());
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.layer == ignoreCollisionBulletLayer)
        {
            return;
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
