using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterRigController : MonoBehaviour
{
	private float coefLerpRig = 0.05f;

	[Header("Rig head/chest REST State")]
	[SerializeField] private Transform rigHeadChestTarget;
	[SerializeField] private Rig rigHeadChest;
	[SerializeField] private Transform head;
	
	[Header("Rig arms Grab weapon")]
	[SerializeField] private Transform targetLeftArm;
	[SerializeField] private Transform hintLeftArm;
	[SerializeField] private Rig rigArms;

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

	public void ResetRigHeadChest(bool withSmooth)
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

	public void SetTargetLeftArm(Vector3 newTarget)
	{
		if (rigArms.weight == 0)
		{
			rigArms.weight = 1;
		}
		targetLeftArm.position = newTarget;
	}

	public void ResetRigArms()
	{
		rigArms.weight = 0;
	}
}
