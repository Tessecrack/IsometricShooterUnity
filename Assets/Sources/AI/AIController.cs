using UnityEngine;

public class AIController : MonoBehaviour
{
	private Transform agentTransform;

	private Transform targetTransform;

	private readonly int rangeVision = 8;

	public bool IsPlayerFounded { get; private set; }

	private int targetLayerMask;

	private AIController(Transform agentTransform, Transform target, int targetLayerMask)
	{
		this.agentTransform = agentTransform;
		this.targetTransform = target;
		this.targetLayerMask = targetLayerMask;
	}

	public static AIController InitAIController(Transform agentTransform, Transform target, int targetLayerMask)
	{
		var agent = new AIController(agentTransform, target, targetLayerMask);
		return agent;
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
			IsPlayerFounded = true;
		}
	}

	public Vector3 GetTargetPosition()
	{
		SearchTarget();

		return IsPlayerFounded && targetTransform != null ? targetTransform.position : Vector3.zero;
	}
}
