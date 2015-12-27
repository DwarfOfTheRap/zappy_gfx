using UnityEngine;
using System.Collections;

public class PlayerLegsScript : MonoBehaviour {
	ParticleSystem trailSparks;
	// Use this for initialization
	void Awake() {
		trailSparks = GetComponentInChildren<ParticleSystem>();
		Debug.Log (trailSparks);
	}

	public void DisableSparksAnimation()
	{
		trailSparks.Stop();
	}

	public void EnableSparksAnimation(Orientation orientation)
	{
		trailSparks.transform.rotation = OrientationManager.GetRotation (orientation);
		trailSparks.Play ();
	}
}
