using System;
using UnityEngine;

public class AIController
{
	public bool IsPlayerFounded { get; private set; }

	public bool CanAttack { get; private set; }

	private Transform agentTransform;

	private Transform targetTransform;

	private readonly int rangeVision = 10;

	private readonly int distanceVision = 15;
	
	private int targetLayerMask;

	public Action OnTargetFound;

	private AIController(Transform agentTransform, Transform target, int targetLayerMask)
	{
		this.agentTransform = agentTransform;
		this.targetTransform = target;
		this.targetLayerMask = targetLayerMask;
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

		if (!IsPlayerFounded && Physics.CheckSphere(agentTransform.position, rangeVision, 1 << this.targetLayerMask))
		{
			OnTargetFound?.Invoke();
			IsPlayerFounded = true;
		}

		var start = new Vector3(agentTransform.position.x, agentTransform.position.y + 1, agentTransform.position.z);
		CanAttack = Physics.Raycast(start, agentTransform.forward, distanceVision, 1 << targetLayerMask);
	}

	public Vector3 GetTargetPosition()
	{
		SearchTarget();

		return IsPlayerFounded && targetTransform != null ? targetTransform.position : Vector3.zero;
	}

	public void TakeDamage(int damage)
	{
		IsPlayerFounded = true;
		CanAttack = true;
	}
}
