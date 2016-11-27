using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

[RequireComponent(typeof(Bloom))]
[RequireComponent(typeof(VignetteAndChromaticAberration))]
[RequireComponent(typeof(Antialiasing))]
[RequireComponent(typeof(UnityStandardAssets.ImageEffects.PostEffectsBase))]
public class ImageEffectManager : MonoBehaviour {
	private float[]		_cullDistance;

	void Start()
	{
		this._cullDistance = new float[6]{ 30, 60, 90, 120, 150, 180};
		OnQualityChange(QualityManager.GetQualityLevel());
	}

	void OnEnable()
	{
		QualityManager.OnQualityChange += OnQualityChangeHandler;
	}

	void OnDisable()
	{
		QualityManager.OnQualityChange -= OnQualityChangeHandler;
	}

	void OnQualityChange(int qualityLevel)
	{
		var distances = new float[32];
		distances[10] = this._cullDistance[qualityLevel];
		GetComponentInChildren<SkyboxCamera>().enabled = qualityLevel > 0;
		GetComponent<Bloom>().enabled = qualityLevel > 2;
		GetComponent<VignetteAndChromaticAberration>().enabled = qualityLevel > 3;
		GetComponent<Antialiasing>().enabled = qualityLevel > 3;
		GetComponent<PostEffectsBase>().enabled = qualityLevel > 2;
		GetComponent<Camera>().layerCullDistances = distances;
	}

	void OnQualityChangeHandler(QualityEventArg arg)
	{
		int qualityLevel = arg.qualityLevel;
		OnQualityChange (qualityLevel);
	}
}
