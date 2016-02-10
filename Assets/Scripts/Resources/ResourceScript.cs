using UnityEngine;
using System.Collections;

public class ResourceScript : MonoBehaviour, IResourceEnabler {
	public ResourceController controller;

	void OnEnable()
	{
		Animation animation = GetComponentInChildren<Animation>();
		foreach (AnimationState state in animation)
		{
			state.time = Random.Range (0, state.length);
		}
		animation.Play();
	}

	void Awake()
	{
		controller = new ResourceController(this);
	}

	public void Enable (bool state)
	{
		this.gameObject.SetActive(state);
	}

	void Update()
	{
		controller.Update ();
	}
}

public class ResourceController
{
	public uint			count;
	IResourceEnabler	enabler;

	public ResourceController(IResourceEnabler enabler)
	{
		this.enabler = enabler;
	}

	void Enable()
	{
		enabler.Enable (count > 0);
	}

	public void Update()
	{
		Enable ();
	}
}

public interface IResourceEnabler {
	void Enable(bool state);
}