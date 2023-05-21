using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterRigController : MonoBehaviour
{
	[Header("Rig head/chest REST State")]
	[SerializeField] private Transform rigHeadChestTarget;
	[SerializeField] private Rig rigHeadChest;
	[SerializeField] private Transform head;
	private float coefLerpRigHeadChest = 0.05f;

	public void SetTargetHeadChestRig(Vector3 newTarget)
	{
		if (rigHeadChest.weight < 1)
		{
			rigHeadChest.weight = Mathf.Lerp(rigHeadChest.weight, 1, coefLerpRigHeadChest);
		}
		if (rigHeadChest.weight > 0.9)
		{
			rigHeadChest.weight = 1;
		}
		newTarget.y = head.position.y;
		rigHeadChestTarget.position = newTarget;
	}

	public void ResetHeadChesRig(bool withSmooth)
	{
		if (withSmooth)
		{
			rigHeadChest.weight = Mathf.Lerp(rigHeadChest.weight, 0, coefLerpRigHeadChest);
		}
		else
		{
			rigHeadChest.weight = 0;
		}
	}
}
