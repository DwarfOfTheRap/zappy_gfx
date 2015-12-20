using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class AnimationTestScript : MonoBehaviour {
	public string animationName;
	public bool	animationIsPlaying;
	private Animator animator;

	void Start()
	{
		this.animator = GetComponent<Animator>();
	}

	private bool IsPlaying()
	{
		return animator.GetCurrentAnimatorStateInfo(0).IsName (animationName);
	}

	void Update()
	{
		animationIsPlaying = IsPlaying ();
	}
}
