using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterRigController : MonoBehaviour
{
	private float coefLerpRig = 0.05f;

	[Header("Rig head/chest REST State")]
	[SerializeField] private Transform rigHeadChestTarget;
	[SerializeField] private Transform head;
	[SerializeField] private Rig rigHeadChest;
	
	[Header("Rig left arm Grab weapon")]
	[SerializeField] private Transform targetLeftArm;
	[SerializeField] private Transform hintLeftArm;
	[SerializeField] private Rig rigLeftArm;

	[Header("Rig right arm Grab melee weapon")]
	[SerializeField] private Transform targetRightArm;
	[SerializeField] private Transform hintRightArm;
	[SerializeField] private Rig rigRightArm;

	public void SetTargetHeadChestRig(Vector3 newTarget)
	{
		if (rigHeadChest.weight < 1)
		{
			rigHeadChest.weight = Mathf.Lerp(rigHeadChest.weight, 1, coefLerpRig);
		}
		if (rigHeadChest.weight > 0.9)
		{
			rigHeadChest.weight = 1;
		}
		newTarget.y = head.position.y;
		rigHeadChestTarget.position = newTarget;
	}

	public void ResetRigHeadChest(in bool withSmooth)
	{
		if (withSmooth)
		{
			rigHeadChest.weight = Mathf.Lerp(rigHeadChest.weight, 0, coefLerpRig);
		}
		else
		{
			rigHeadChest.weight = 0;
		}
	}

	public void SetTargetLeftArm(in Vector3 newTarget)
	{
		if (rigLeftArm.weight == 0)
		{
			rigLeftArm.weight = 1;
		}
		targetLeftArm.position = newTarget;
	}

	public void ResetRigLeftArm()
	{
		rigLeftArm.weight = 0;
	}

	public void SetRigRightArm()
	{
		rigRightArm.weight = 1;
	}

	public void ResetRigRightArm()
	{
		rigRightArm.weight = 0;
	}
}
