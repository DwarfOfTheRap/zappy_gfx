using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class ImageEffectManager : MonoBehaviour {
	void OnEnable()
	{
		QualityManager.OnQualityChange += OnQualityChange;
	}

	void OnDisable()
	{
		QualityManager.OnQualityChange -= OnQualityChange;
	}

	void OnQualityChange(QualityEventArg arg)
	{
		int qualityLevel = arg.qualityLevel;
		GetComponentInChildren<SkyboxCamera>().enabled = qualityLevel > 0;
		GetComponent<Bloom>().enabled = qualityLevel > 2;
		GetComponent<VignetteAndChromaticAberration>().enabled = qualityLevel > 3;
		GetComponent<Antialiasing>().enabled = qualityLevel > 3;
		GetComponent<CameraMotionBlur>().enabled = qualityLevel > 4;
	}
}
