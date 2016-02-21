using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class ImageEffectManager : MonoBehaviour {

	void ManageQualitySettings()
	{
		int qualityLevel = QualitySettings.GetQualityLevel();
		GetComponent<Bloom>().enabled = qualityLevel > 2;
		GetComponent<VignetteAndChromaticAberration>().enabled = qualityLevel > 3;
		GetComponent<Antialiasing>().enabled = qualityLevel > 3;
		GetComponent<CameraMotionBlur>().enabled = qualityLevel > 4;
	}
	void Update()
	{
		ManageQualitySettings ();
	}
}
