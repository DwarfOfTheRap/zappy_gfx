using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public class IncantationScript : MonoBehaviour {
	private ParticleSystem	_ps;
	private Color			_color;
	public	Color			startColor;
	// Use this for initialization
	void Start () {
		this._ps = GetComponent<ParticleSystem>();
		_ps.playbackSpeed = (GameManagerScript.instance.timeManager.timeSpeed / 10);
		_ps.startColor = startColor;
	}
	
	// Update is called once per frame
	void Update () {
		var list = new ParticleSystem.Particle[_ps.particleCount];
		_ps.GetParticles (list);
		for (int i = 0; i < list.Length; i++)
			list[i].color = Color.Lerp (Color.clear, startColor, list[i].lifetime / list[i].startLifetime);
		_ps.SetParticles (list, _ps.particleCount);
		if (!_ps.IsAlive())
		{
			Destroy (gameObject);
		}
	}
}
