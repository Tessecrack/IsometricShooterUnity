using System;
using UnityEngine;

public class AIController
{
	public bool IsPlayerFounded { get; private set; }

	private Transform agentTransform;

	private Transform targetTransform;

	private readonly int rangeVision = 8;
	
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
			IsPlayerFounded = false;
			return;
		}

		if (Physics.CheckSphere(agentTransform.position, rangeVision, 1 << this.targetLayerMask))
		{
			OnTargetFound?.Invoke();
			IsPlayerFounded = true;
		}
	}

	public Vector3 GetTargetPosition()
	{
		SearchTarget();

		return IsPlayerFounded && targetTransform != null ? targetTransform.position : Vector3.zero;
	}
}
