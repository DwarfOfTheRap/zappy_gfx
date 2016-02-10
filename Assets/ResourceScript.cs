using UnityEngine;
using System.Collections;

public class ResourceScript : MonoBehaviour {
	void OnEnable()
	{
		Animation animation = GetComponentInChildren<Animation>();
		foreach (AnimationState state in animation)
		{
			state.time = Random.Range (0, state.length);
		}
		animation.Play();
	}
}