using UnityEngine;

public class AIAgent : MonoBehaviour
{
	private bool hasTarget = false;
	private Vector3 targetPosition = Vector3.zero;

	public virtual void SetTargetPosition(Vector3 target)
	{
		this.targetPosition = target;
	}

	public void SearchTarget()
	{

	}
}
