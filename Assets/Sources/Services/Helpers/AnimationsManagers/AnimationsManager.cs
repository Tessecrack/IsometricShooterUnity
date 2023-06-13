using UnityEngine;

public abstract class AnimationsManager : MonoBehaviour
{
	protected Animator animator;
	protected CharacterAnimationState currentAnimationState;

	void Start()
    {
		animator = GetComponent<Animator>();
		currentAnimationState = new CharacterAnimationState();
	}

	public abstract void ChangeAnimationsState(CharacterAnimationState updatedAnimationsState);
}
