using UnityEngine;

public class CharacterRigController : MonoBehaviour
{
	[SerializeField] private Transform headTarget;

	public void SetHeadTarget(Vector3 target)
	{
		if (headTarget == null)
		{
			return;
		}

		this.headTarget.position = target;
	}
}
