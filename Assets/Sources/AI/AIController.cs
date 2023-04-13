using System;
using UnityEngine;

public class AIController
{
	public bool IsPlayerFounded { get; private set; }
	public bool CanAttack { get; private set; }

	private Transform agentTransform;

	private Transform targetTransform;

	private readonly int rangeVision = 8;
	
	private int targetLayerMask;

	public Action OnTargetFound;

	public Action OnAttack;

	private AIController(Transform agentTransform, Transform target, int targetLayerMask)
	{
		this.agentTransform = agentTransform;
		this.targetTransform = target;
		this.targetLayerMask = targetLayerMask;

		OnTargetFound += TargetFound;
		OnAttack += Attack;
	}

	public static AIController InitAIController(Transform agentTransform, Transform targetTransform, int targetLayerMask)
	{
		return new AIController(agentTransform, targetTransform, targetLayerMask);
	}

	private void SearchTarget()
	{
		if (targetTransform == null)
		{
			CanAttack = false;
			IsPlayerFounded = false;
			return;
		}

		if (!IsPlayerFounded && LookAround())
		{
			OnTargetFound?.Invoke();
		}

		if (IsPlayerFounded)
		{
			OnAttack?.Invoke();
		}
	}

	public Vector3 GetTargetPosition()
	{
		SearchTarget();

		return (CanAttack || IsPlayerFounded) && targetTransform != null ? targetTransform.position : Vector3.zero;
	}

	public void TakeDamage(int damage)
	{
		OnTargetFound?.Invoke();
		OnAttack?.Invoke();
	}

	private bool LookAround() => Physics.CheckSphere(agentTransform.position, rangeVision, 1 << this.targetLayerMask);

	private void TargetFound()
	{
		IsPlayerFounded = true;
	}

	private void Attack()
	{
		CanAttack = true;
	}
}
