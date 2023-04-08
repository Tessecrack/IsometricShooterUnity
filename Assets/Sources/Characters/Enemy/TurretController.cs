using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour // THIS WILL BE REMOVED LATER
{
	[SerializeField] private List<GameObject> muzzles;

	[SerializeField] private Bullet bullet;

	private ActorHealth health;

	private readonly float speedAttack = 50.0f;

	private readonly float damage = 25.0f;

	private AIController agent;

	private int delayBetweenFire = 1;
	private float passedTimeFire = 0.0f;

	private bool canAttack = true;

	private bool isPlayedSound = false;

	private readonly float speedRotation = 10.0f;

	private AudioSource audioMovement;
	private void Start()
	{
		var player = FindObjectOfType<PlayerController>();
		agent = AIController.InitAIController(this.transform, player.transform, player.gameObject.layer);
		passedTimeFire = delayBetweenFire;
		health = new ActorHealth();
		audioMovement = GetComponent<AudioSource>();
	}

	private void Update()
	{
		UpdateRotation();
		UpdateAttack();
		UpdateTimeAttack();
	}

	private void UpdateRotation()
	{
		var targetPos = agent.GetTargetPosition();
		if (agent.IsPlayerFounded)
		{
			this.transform.forward = Vector3.RotateTowards(this.transform.forward,
				targetPos - this.transform.position,
				Time.deltaTime * speedRotation,
				0.0f);
		}
	}

	private void UpdateAttack()
	{
		if (agent.IsPlayerFounded && canAttack)
		{
			if (!isPlayedSound)
			{
				audioMovement.Play();
				isPlayedSound = true;
			}
			StartFire();
			canAttack = false;
		}
	}

	private void UpdateTimeAttack()
	{
		if (!canAttack)
		{
			passedTimeFire += Time.deltaTime;
		}
		if (passedTimeFire >= delayBetweenFire)
		{
			canAttack = true;
			passedTimeFire = 0.0f;
		}
	}

	public void TakeDamage(float damage)
	{
		health.TakeDamage(damage);
		if (health.IsDead)
		{
			Destroy(this.gameObject);
		}
	}

	private void StartFire()
	{
		StartCoroutine(Fire());
	}

	IEnumerator Fire()
	{
		foreach(var muzzle in muzzles) 
		{
			var instanceCurrentBullet = Instantiate<Bullet>(bullet, muzzle.transform.position, muzzle.transform.rotation);
			instanceCurrentBullet.StartFire(this, agent.GetTargetPosition(), speedAttack, damage);
			yield return new WaitForSeconds(Time.deltaTime);
		}
	}
}
