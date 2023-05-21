using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterRigController : MonoBehaviour
{
	[Header("Rig head/chest REST State")]
	[SerializeField] private Transform rigHeadChestTarget;
	[SerializeField] private Rig rigHeadChest;
	[SerializeField] private Transform head;

	public void SetTargetHeadChestRig(Vector3 newTarget)
	{
		if (rigHeadChest.weight == 0)
		{
			rigHeadChest.weight = 1;
		}
		newTarget.y = head.position.y;
		rigHeadChestTarget.position = newTarget;
	}

	public void ResetHeadChesRig()
	{
		rigHeadChest.weight = 0;
	}
}
